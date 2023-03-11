using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;


namespace KodaKoziol2263Ex9B
{
    internal class EnemyFormationManager
    {
        /**
         * Koda Koziol: Objects of this class manage the positions and movement of an array of EnemyShipSprites and its projectiles.
         */

        internal int
            numberOfRemainingEnemies;
        internal float downmostEnemyPositionY, leftmostEnemyPositionX, rightmostEnemyPositionX;
        internal int GetNumberDestroyed
        {
            get {return columns * rows - numberOfRemainingEnemies; }
        }

        int columns, rows;
        static float
            SPEED_X = 1 * GameSession.ResolutionFactor,
            SPEED_Y = 1 * GameSession.ResolutionFactor;
        bool isMovingRight = true;


        Transform formationArea, range;
        internal EnemyShipSprite[,] formation { get; private set; }
        internal ProjectilesManager projectilesManager;

        const int FIRE_TIMER_INTERVAL = 120; // 2 seconds with a GameLoop.UPDATE_INTERVAL of 1000/60 ms
        const int TIME_BEFORE_FIRST_SHOT = (FIRE_TIMER_INTERVAL*3)/2;
        int fireTimeElapsed = 0;
        
        internal EnemyFormationManager(Transform transform, int columns, int rows, Transform range)
        {
            /**
             * Koda Koziol: This constructor method initialize positions of a grid of EnemyShips.
             */

            formationArea = transform;
            this.columns = columns;
            this.rows = rows;
            this.range = range;

            formation = new EnemyShipSprite[columns, rows];
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    Vector2 enemyShipPosition = new Vector2(
                        formationArea.position.X + c * (formationArea.scale.X / columns),
                        formationArea.position.Y + r * (formationArea.scale.Y / rows)
                        );
                    formation[c,r] = new EnemyShipSprite(enemyShipPosition);
                }
            }
            numberOfRemainingEnemies = formation.Length;

            projectilesManager = new ProjectilesManager(3, Color.LightGreen, 15f, range);
        }
        void ContinueFiring()
        {
            /**
             * Koda Koziol: This method, called regularly,
             *              fires a projectile with the Fire() method once every FIRE_TIMER_INTERVAL.
             */

            fireTimeElapsed++;
            if (fireTimeElapsed >= FIRE_TIMER_INTERVAL && fireTimeElapsed >= TIME_BEFORE_FIRST_SHOT)
            {
                Fire();
                fireTimeElapsed = 0;
            }

        }
        void Fire()
        {
            /**
             * Koda Koziol: This method instantiates a projectile (if there is a free one in the projectileManager's pool)
             *              at the position of the lowest enabled enemy ship in a randomly selected column in the formation.
             */

            int currentlyFiringColumn = RandomNumberGenerator.GetInt32(columns);
            for (int r = rows - 1; r >= 0; r--)
            {
                if (formation[currentlyFiringColumn, r].Enabled)
                {
                    EnemyShipSprite activelyFiringEnemyShip = formation[currentlyFiringColumn, r];

                    Vector2 projectileSpawnPoint = new Vector2(
                        activelyFiringEnemyShip.transform.position.X + (activelyFiringEnemyShip.transform.scale.X / 2f) - (ProjectileSprite.Size.Width / 2f),
                        activelyFiringEnemyShip.transform.position.Y
                        );

                    projectilesManager.InstatiateProjectile(projectileSpawnPoint);
                    return;
                }
            }
        }

        void Move()
        {
            /**
             * Koda Koziol: This method, called regularly, will move every ship in the formation down and back-and-forth in a zig-zag pattern.
             */

            //Initialize downmostEnemyPositionY, leftmostEnemyPositionX, and rightmostEnemyPositionX to any enabled enemy
            foreach(EnemyShipSprite enemyShip in formation)
            {
                if(enemyShip.Enabled)
                {
                    downmostEnemyPositionY = formation[0, 0].transform.position.Y;
                    leftmostEnemyPositionX = rightmostEnemyPositionX = formation[0, 0].transform.position.X;
                    break;
                }
            }

            //Find downmostEnemyPositionY, leftmostEnemyPositionX, and rightmostEnemyPositionX
            foreach (EnemyShipSprite enemyShip in formation)
            {
                if (enemyShip.Enabled)
                {
                    if (enemyShip.transform.position.Y > downmostEnemyPositionY)
                        downmostEnemyPositionY = enemyShip.transform.position.Y;
                    if (enemyShip.transform.position.X < leftmostEnemyPositionX)
                        leftmostEnemyPositionX = enemyShip.transform.position.X;
                    if (enemyShip.transform.position.X > rightmostEnemyPositionX)
                        rightmostEnemyPositionX = enemyShip.transform.position.X;
                }
            }

            //Move enemies
            foreach (EnemyShipSprite enemyShip in formation)
            {
                if (enemyShip.Enabled)
                {
                    //Set Y position
                     enemyShip.transform.position.Y += SPEED_Y;

                    //Set X position
                    if (isMovingRight)
                    {
                        enemyShip.transform.position.X += SPEED_X;
                        if(rightmostEnemyPositionX >= range.Right - EnemyShipSprite.Size.Width)
                            isMovingRight = false;
                    }
                    if(!isMovingRight)
                    {
                        enemyShip.transform.position.X -= SPEED_X;
                        if(leftmostEnemyPositionX <= range.Left)
                            isMovingRight = true;
                    }
                }
            }
        }

        void CheckForCollisions(ProjectileSprite[] playerProjectilePool, PlayerShipManager playerShip)
        {
            /**
             * Koda Koziol: This method checks if a player projectile is touching any enemyShip,
             *              calling PerformExplosion on that ship if that's the case.
             */

            numberOfRemainingEnemies = 0;
            foreach (EnemyShipSprite enemyShip in formation)
            {
                if (enemyShip.Enabled)
                {
                    numberOfRemainingEnemies++;

                    //player projectiles
                    foreach (ProjectileSprite playerProjectile in playerProjectilePool)
                    {
                        if (playerProjectile.Enabled && enemyShip.transform.IntersectsWith(playerProjectile.transform))
                        {
                            playerProjectile.Enabled = false;
                            enemyShip.PerformExplosion();
                        }
                    }
                    //player ship
                    if (enemyShip.collisionsEnabled && enemyShip.transform.IntersectsWith(playerShip.playerShipSprite.transform))
                    {
                        enemyShip.collisionsEnabled = false;
                        playerShip.LoseALife();
                        enemyShip.PerformExplosion();
                    }
                }
            }
        }


        internal void Update(ProjectileSprite[] playerProjectilePool, PlayerShipManager playerShip)
        {
            /**
             * Koda Koziol: This method updates all the properties of the EnemyFormationManager object.
             * 
             *              It should be called in a GameSession Update() method.
             */

            ContinueFiring();
            projectilesManager.Update();
            Move();
            CheckForCollisions(playerProjectilePool, playerShip);
        }

        internal void DrawSprites(Graphics graphics)
        {
            /**
             * Koda Koziol: This method draws everything in this EnemyFormationManager object.
             */

            for ( int c = 0; c < columns; c++)
            {
                for(int r = 0; r < rows; r++)
                    formation[c, r].Draw(graphics);
            }
            projectilesManager.DrawSprites(graphics);
        }
    }
}
