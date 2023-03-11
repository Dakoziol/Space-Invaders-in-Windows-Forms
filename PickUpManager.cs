using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal class PickUpManager
    {
        /**
         * Koda Koziol: an object of this class manages the instantiation, destruction, and movement of "Pick Ups"
         * 
         *              For example, a heart that floats down from the top of the display periodically,
         *              which when the player collides with it, restores one life.
         * 
         *              I didn't get around to implementing this.
         */

        HeartSprite heart; //Grants the player one extra life.
        StarSprite star; //Grants the player more firepower for a limited time. Bigger stronger projectiles? Double firerate? Fire two projectiles at a time?
        CoffeeSprite coffee; //Slows down every object in the game, except the player and it's projectiles for a limited time.
    }
}
