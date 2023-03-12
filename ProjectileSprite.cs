using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal class ProjectileSprite : Sprite
    {
        /**
         * Koda Koziol: This class contains the properties needed to Draw a Projectile.
         */

        internal static Size Size = new Size(5, 40);

        Color color;
        Pen pen;


        internal ProjectileSprite(Color color)
        {
            /**
             * Koda Koziol: This constructor method initializes the position and size related properties.
             */

            Enabled = false;

            transform.scale.X = Size.Width;
            transform.scale.Y = Size.Height;

            this.color = color;
        }

        internal override void Draw(Graphics graphics)
        {
            /**
             * Koda Koziol: This method takes a Graphics object and draws the ship to it.
             */

            if (Enabled)
                graphics.DrawRectangle(new Pen(color, 2.5f * GameSession.ResolutionFactor), transform.RectangleScaledForGraphics(graphics));
        }

    }
}
