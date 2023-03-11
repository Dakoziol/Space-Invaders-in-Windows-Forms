using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodaKoziol2263Ex9B
{
    internal class GameSessionEndedEventArgs : EventArgs
    {
        /**
         * Koda Koziol: This custom event arguments class derives from the EventArgs base class.
         */

        internal int livesRemaining;
        internal int killCount;
        internal GameSessionEndedEventArgs(int remainingLives, int killCount)
        {
            /**
             * Koda Koziol: The constructor method sets the remainingLives and killCount integer properties.
             */

            this.livesRemaining = remainingLives;
            this.killCount = killCount;
        }
    }
}
