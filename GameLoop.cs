using System.Text;
using Timer = System.Timers.Timer;

namespace KodaKoziol2263Ex9B
{
    internal abstract class GameLoop
    {
        /**
         * Koda Koziol: The GameLoop class is the abstract base class for the GameSession class,
         *              managing the timing of the Start(), Update(), and DrawEverything() method calls.
         */

        internal static int UPDATE_INTERVAL = 1000 / 60; // 60 frames per second

        bool paused = false;

        protected Timer updateTimer = new Timer();

        Bitmap bitmap;
        protected PictureBox pictureBox;
        protected Graphics bufferGraphics;

        internal GameLoop(PictureBox pictureBox)
        {
            /**
             * Koda Koziol: This constructor method initializes the graphics and backgroundColor properties.
             */

            this.pictureBox = pictureBox;
        }

        /**
         * Koda Koziol: The following methods set and read the paused boolean property.
         */
        internal void Pause() => paused = true;
        internal void UnPause() => paused = false;
        internal bool IsPaused() => paused;

        internal void Begin()
        {
            /**
             * Koda Koziol: This method begins the GameLoop by calling Start() and starting the updateTimer.
             */

            Start();

            //Initialize and Start timer
            updateTimer.AutoReset = true;
            updateTimer.Elapsed += UpdateTimer_Tick;
            updateTimer.Interval = UPDATE_INTERVAL;
            updateTimer.Start();

            bitmap = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            bufferGraphics = Graphics.FromImage(bitmap);
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            /**
             * Koda Koziol: This Timer tick event method runs the next gameLoop, redrawing everything.
             */

            if(!paused)
            {
                Update();
                pictureBox.Invalidate();
            }
        }

        /**
         * Koda Koziol: The following virtual methods, Start(), Update(), and DrawEverything() are overridden by the derived class, GameSession.
         *              See the GameSession class for descriptions of each method.
         */
        protected virtual void Start() { }
        protected virtual void Update() { }
        internal virtual void DrawEverything(Graphics graphics) { }
    }
}
