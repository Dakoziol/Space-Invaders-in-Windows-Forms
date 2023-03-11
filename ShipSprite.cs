using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal abstract class ShipSprite : Sprite
    {
        /**
         * Koda Koziol: This is the abstract base class for all ship objects, containing all the properties needed to Draw a Ship.
         */

        protected Color color;
        internal static Size Size = new Size(40, 40);

        internal void UpdateColor(Color color)
        {
            /**
             * Koda Koziol: This method takes a Color object and sets the color property.
             */

            this.color = color;
        }

        internal override void Draw(Graphics graphics)
        {
            /**
             * Koda Kozio: This method takes a Graphics object and Draws the ship to it.
             * 
             *              It just draws a rectangle right now.
             */

            if (Enabled)
                graphics.DrawRectangle(new Pen(color, 5 * GameSession.SpacialScalar), transform.RectangleScaledForGraphics(graphics));
        }
    }
}
