using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal class EnemyShipSprite : ShipSprite
    {
        /**
         * Koda Koziol: This EnemyShipSprite class derives from the ShipSprite class, and through it contains all the properties needed to Draw a Ship.
         */

        internal bool collisionsEnabled = true;


        internal EnemyShipSprite(Vector2 position)
        {
            /**
             * Koda Koziol: This constructor method initializes the position and size related properties.
             */

            //These are different for every ship
            transform.position = position;

            // These are the same for every ship
            transform.scale.X = Size.Width;
            transform.scale.Y = Size.Height;
            
            color = Color.MediumPurple;

        }

        internal async void PerformExplosion() => await Task.Run(Explosion); // Learned from this: https://www.c-sharpcorner.com/article/async-and-await-in-c-sharp/
        void Explosion()
        {
            /**
             * Koda Koziol: This Task method changes the color of the sprite and increases its size before disabling it
             *              to look vaguely like an explosion.
             */

            UpdateColor(Color.Orange);
            int duration = 10;
            for (int t = 0; t < duration; t++)
            {
                transform.scale.X += 2 * (duration - t);
                transform.scale.Y += 2 * (duration - t);
                transform.position.X -= duration - t;
                transform.position.Y -= 2 * (duration - t);

                Task.Delay(GameLoop.UPDATE_INTERVAL).Wait();
            }
            Enabled = false;
        }

    }
}
