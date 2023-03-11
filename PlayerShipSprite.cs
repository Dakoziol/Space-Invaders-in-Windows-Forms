using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal class PlayerShipSprite : ShipSprite
    {
        /**
         * Koda Koziol: This PlayerShipSprite class derives from the ShipSprite class,
         *              and through it contains all the properties needed to Draw a Ship.
         */

        internal PlayerShipSprite(Color color)
        {
            /**
             * Koda Koziol: This constructor method initializes the position and size related properties.
             */

            transform.scale.X = Size.Width;
            transform.scale.Y = Size.Height;

            this.color = color;
        }
    }
}

