using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KodaKoziol2263Ex9B
{
    internal class PlayerShipManager
    {
        /**
         * Koda Koziol: Objects of this class manage the position and movement of a PlayerShipSprite and its projectiles.
         */

        const int PROJECTILE_POOL_SIZE = 10;
        static float
            PROJECTILE_VELOCITY_Y = -15,
            PLAYER_SHIP_SPEED = 5 * GameSession.ResolutionFactor;

        internal bool enabled = true;
        internal PlayerShipSprite playerShipSprite { get; private set; }
        Transform range;

        internal int remainingLives { get; private set; }
        Color[] healthColorArray = { Color.Gray, Color.IndianRed, Color.OrangeRed, Color.Orange }; // To desaturate the sprite as it takes on more damage.

        internal ProjectilesManager projectileManager { get; private set; }
 
        internal PlayerShipManager(Vector2 initialPosition, Transform range, int remainingLives)
        {
            /**
             * Koda Koziol: This constructor method initializes the playerShipSprite, range property, and projectileManager.
             */

            playerShipSprite = new PlayerShipSprite(healthColorArray[remainingLives]);

            playerShipSprite.transform.position = initialPosition;

            this.range = range;
            this.remainingLives = remainingLives;
            
            projectileManager = new ProjectilesManager(PROJECTILE_POOL_SIZE, Color.LightCyan, PROJECTILE_VELOCITY_Y, range);
        }

        void Fire()
        {
            /**
             * Koda Koziol: This method fires a projectile from the location of the middle of the playerShipSprite
             */

            Vector2 projectileSpawnPoint = new Vector2(
                playerShipSprite.transform.position.X + (playerShipSprite.transform.scale.X / 2) - (ProjectileSprite.Size.Width / 2),
                playerShipSprite.transform.position.Y
                );
            projectileManager.InstatiateProjectile(projectileSpawnPoint);
        }


        void Move(bool ShouldGoLeft, bool ShouldGoRight)
        {
            /**
             * Koda Koziol: Ths method takes the booleans goLeft and goRight, and with them moves the playerShipSprite left or right.
             */

            if (ShouldGoLeft && playerShipSprite.transform.Left >= 0)
                playerShipSprite.transform.position.X -= PLAYER_SHIP_SPEED;
            else if (ShouldGoRight && playerShipSprite.transform.Right <= range.scale.X)
                playerShipSprite.transform.position.X += PLAYER_SHIP_SPEED;
        }


        void CheckForCollisions(EnemyFormationManager enemyFormation)
        {
            /**
             * Koda Koziol: This method checks if an enemy projectile is touching the playerShip, decrementing remainingLives.
             */

            foreach (ProjectileSprite enemyProjectile in enemyFormation.projectilesManager.pool)
            {
                if (enemyProjectile.Enabled && playerShipSprite.transform.IntersectsWith(enemyProjectile.transform))
                {
                    if(remainingLives > 1) // Not last life
                        enemyProjectile.Enabled = false;
                    LoseALife();
                }
            }
        }


        internal void LoseALife()
        {
            /**
             * Koda Koziol: This method decriments remainingLives and updates the color of the sprite accordingly.
             */

            if (remainingLives > 1) // Not last life
            {
                remainingLives--;
                playerShipSprite.UpdateColor(healthColorArray[remainingLives]);
                PerformKnockback();
                PerformBlink();
            }
            else
            {
                PerformExplosion();
            }
        }


        /**
         * Koda Koziol: The async Task methods I learned from here: https://www.c-sharpcorner.com/article/async-and-await-in-c-sharp/
         */

        async void PerformKnockback() => await Task.Run(Knockback);
        void Knockback()
        {
            /**
             * Koda Koziol: This Task method moves the sprite down and back up, as if it was pushed back but recovered.
             */

            float initialPositionY = playerShipSprite.transform.position.Y;
            int duration = 15;
            int knockScale = -1;

            for(int t = 0; t < duration; t++)
            {
                //https://www.desmos.com/calculator came in handy for coming up with this equation
                playerShipSprite.transform.position.Y = (t*t - duration * t) * knockScale + initialPositionY;
                Task.Delay(GameLoop.UPDATE_INTERVAL).Wait();
            }
        }

        async void PerformBlink() => await Task.Run(Blink);
        void Blink()
        {
            /**
             * Koda Koziol: This Task method makes the sprite color flash white 3 times.
             */

            int interval = GameLoop.UPDATE_INTERVAL * 10;
            for(int i = 0; i < 3; i++)
            {
                playerShipSprite.UpdateColor(Color.White);
                Task.Delay(interval).Wait();
                playerShipSprite.UpdateColor(healthColorArray[remainingLives]);
                Task.Delay(interval).Wait();
            }
        }

        internal async void PerformExplosion() => await Task.Run(Explosion);
        void Explosion()
        {
            /**
             * Koda Koziol: This Task method changes the color of the sprite and increases its size before disabling it
             *              to look vaguely like an explosion.
             */

            playerShipSprite.UpdateColor(Color.Orange);
            int duration = 5;
            for (int t = 0; t < duration; t++)
            {
                playerShipSprite.transform.scale.X += 2 * (duration - t);
                playerShipSprite.transform.scale.Y += 2 * (duration - t);
                playerShipSprite.transform.position.X -= duration - t;
                playerShipSprite.transform.position.Y -= (duration - t);

                Task.Delay(GameLoop.UPDATE_INTERVAL).Wait();
            }
            enabled = false;
            remainingLives--;
        }

        internal void Update(bool goLeft, bool goRight, ref bool doFire, EnemyFormationManager enemyFormation)
        {
            /**
             * Koda Koziol: This method updates all the properties of the PlayerShipManager object.
             * 
             *              It should be called in a GameSession Update() method.
             */

            Move(goLeft, goRight);

            if (doFire)
            {
                Fire();
                doFire = false;
            }
            projectileManager.Update();
            CheckForCollisions(enemyFormation);
        }

        internal void DrawSprites(Graphics graphics)
        {
            /**
             * Koda Koziol: This method draws everything in this PlayerShip object.
             */

            playerShipSprite.Draw(graphics);
            projectileManager.DrawSprites(graphics);
        }

    }
}
