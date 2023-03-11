using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KodaKoziol2263Ex9B
{
    internal class GameSession : GameLoop
    {
        /**
         * Koda Koziol: The Game class derives from the GameLoop class, overriding its Start(), Update(), and DrawEverthing() methods.
         * 
         *              All of the game logic will be put in these methods to be executed by the GameLoop class
         */

        internal Graphics graphics;
        internal static float INVERSE_DPI_AT_4K = 1f / 216f;
        internal static float SpacialScalar;
        internal static float ResolutionFactor;

        internal bool goLeft, goRight, doFire, SessionEnded = false;
        int difficulty, remainingLives;

        PlayerShipManager playerShip;
        EnemyFormationManager enemyFormation;

        Font levelTextFont;
        SolidBrush levelTextBrush;
        StringFormat levelTextFormat;

        internal GameSession(int dpi, PictureBox pictureBox, int difficulty, int remainingLives) : base(pictureBox)
        {
            /**
             * Koda Koziol: This constructor method takes a PictureBox and an integer, difficulty,
             *              setting difficulty and calling the base class GameLoop with pictureBox.
             */
            SpacialScalar = INVERSE_DPI_AT_4K * dpi;
            ResolutionFactor = pictureBox.Height / 1080f;

            this.difficulty = difficulty;
            this.remainingLives = remainingLives;
            SessionEnded = false;

            levelTextFont = new Font("Impact", 250, GraphicsUnit.Point);
            levelTextBrush = new SolidBrush(Color.FromArgb(20, 0, 20));
            levelTextFormat = new StringFormat();
            levelTextFormat.Alignment = StringAlignment.Center;
            levelTextFormat.LineAlignment = StringAlignment.Center;

            Vector2 playerShipInitialPosition = new Vector2(
                (pictureBox.Width - PlayerShipSprite.Size.Width) * 0.5f,
                pictureBox.Height * 0.85f
                );

            playerShip = new PlayerShipManager(playerShipInitialPosition, Transform.GetTransformFromRectangle(pictureBox.DisplayRectangle), remainingLives);
        }

        protected override void Start()
        {
            /**
             * Koda Koziol: This method, overriding the GameLoop Start() method, runs once when the GameLoop is Started.
             */
            
            Transform enemyFormationTransform = new Transform();

            enemyFormationTransform.scale = new Vector2(
                pictureBox.Width * 0.7f,
                Math.Clamp(pictureBox.Height * 0.3f * difficulty, 1, pictureBox.Height * 2)
                );

            enemyFormationTransform.position = new Vector2(
                (pictureBox.Width - enemyFormationTransform.scale.X) * 0.5f + EnemyShipSprite.Size.Width,
                pictureBox.Top - enemyFormationTransform.scale.Y
                );

            int enemyFormationColumns = Math.Clamp( (int)(difficulty * 1.5), 1, 8 );
            int enemyFormationRows = difficulty * 2;
            enemyFormation = new EnemyFormationManager(enemyFormationTransform, enemyFormationColumns, enemyFormationRows, Transform.GetTransformFromRectangle(pictureBox.DisplayRectangle));
            
        }



        protected override void Update()
        {
            /**
             * Koda Koziol: This method, overriding the GameLoop Update() method, runs at a regular interval after the GameLoop is started.
             * 
             *              The properties of all gameplay objects should be updated here.
             */

            //Update playerShip
            playerShip.Update(goLeft, goRight, ref doFire, enemyFormation);

            //Update enemyFormation
            enemyFormation.Update(playerShip.projectileManager.pool, playerShip);

            //Check properties for win/lose status
            if (enemyFormation.numberOfRemainingEnemies <= 0)
                OnWinner(new GameSessionEndedEventArgs(playerShip.remainingLives, enemyFormation.GetNumberDestroyed));
            if (enemyFormation.downmostEnemyPositionY > (pictureBox.Height * 0.85f + EnemyShipSprite.Size.Height) || playerShip.remainingLives <= 0)
                OnGameOver(new GameSessionEndedEventArgs(playerShip.remainingLives, enemyFormation.GetNumberDestroyed));
        }



        internal override void DrawEverything(Graphics graphics)
        {
            /**
             * Koda Koziol: This method, overriding the GameLoop DrawEverything() method,
             *              executes the Draw() methods of objects in this GameSession that need to be drawn or redrawn.
             */

            graphics.DrawString(difficulty.ToString(), levelTextFont, levelTextBrush, pictureBox.DisplayRectangle, levelTextFormat);
            playerShip.DrawSprites(graphics);
            enemyFormation.DrawSprites(graphics);
        }

        //Event stuff
        internal event EventHandler<GameSessionEndedEventArgs> Winner;
        internal event EventHandler<GameSessionEndedEventArgs> GameOver;
        protected virtual void OnWinner(GameSessionEndedEventArgs e)
        {
            /**
             * Koda Koziol: Calling this method Invokes the Winner EventHandler
             */

            SessionEnded = true;
            Winner?.Invoke(this, e);
        }
        protected virtual void OnGameOver(GameSessionEndedEventArgs e)
        {
            /**
             * Koda Koziol: Calling this method Invokes the GameOver EventHandler
             */

            SessionEnded = true;
            GameOver?.Invoke(this, e);
        }
    }
}
