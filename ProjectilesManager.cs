using System.Numerics;

namespace KodaKoziol2263Ex9B
{
    internal class ProjectilesManager
    {
        /**
         * Koda Koziol: Instances of this class will manage a pool of projectiles.
         * 
         *              For example, the player will fire with an instance of this class with a maximum of say, 3 projectiles.
         *              These projectiles will be reused, hopefully improving performance.
         */

        internal ProjectileSprite[] pool { get; private set; }
        float projectileVelocityY;
        Transform range;
        internal ProjectilesManager(int poolSize, Color projectileColor, float projectileVelocityY, Transform range)
        {
            /**
             * Koda Koziol: This constructor method initializes projectileVelocityY, range, and pool variables.
             */

            pool = new ProjectileSprite[poolSize];
            for (int i = 0; i < poolSize; i++)
                pool[i] = new ProjectileSprite(projectileColor);

            this.range = range;
            this.projectileVelocityY = projectileVelocityY * GameSession.ResolutionFactor;
        }

        internal void InstatiateProjectile(Vector2 spawnPosition)
        {
            /**
             * Koda Koziol: This method takes an available (disabled) projectile from the pool if one exists,
             *              resets its position and velocity, and enables it.
             * 
             *              If there are no available Projectiles, do nothing (for now).
             */

            int i = 0;
            bool freeProjectileFound = false;
            while (i < pool.Length && !freeProjectileFound)
            {
                if (!pool[i].Enabled)
                {
                    pool[i].transform.position = spawnPosition;
                    pool[i].Enabled = true;
                    freeProjectileFound = true;
                }
                i++;
            }
        }

        internal void Update()
        {
            /**
             * Koda Koziol: This method increments the projectiles while checking if any enabled projectiles have
             *              made it outside the the play area, in which case they are disabled,
             *              freeing them up to be instantiated again.
             */

            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].Enabled)
                {
                    if (!pool[i].transform.IntersectsWith(range))
                        pool[i].Enabled = false;
                    else
                        pool[i].transform.position.Y += projectileVelocityY;
                }
            }
        }

        internal void DrawSprites(Graphics graphics)
        {
            /**
             * Koda Koziol: This method Draws all of the ProjectileSprites in the pool.
             */

            for (int i = 0; i < pool.Length; i++)
                pool[i].Draw(graphics);

        }
    }
}
