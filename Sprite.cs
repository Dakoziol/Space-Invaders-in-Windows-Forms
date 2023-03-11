using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal abstract class Sprite
    {
        /**
         * Koda Koziol: This is the abstract class for all Sprites (visable, non-text objects).
         */

        internal bool Enabled = true;
        internal Transform transform = new Transform(); //This is for defining the position and bounds of a sprite.
                                                        //I happen to be using this to draw rectangles, but even if I were to use images
                                                        //I'd still need a transform Rectangle to size and position them.

        /**
         * Koda Koziol: Objects derived from this class should override this method,
         *              taking a Graphics object and Drawing that sprite to it there.
         */
        internal abstract void Draw(Graphics graphics);
    }
}
