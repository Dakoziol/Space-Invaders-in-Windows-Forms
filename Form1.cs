namespace KodaKoziol2263Ex9B
{
    public partial class Form1 : Form
    {

        const float ASPECT_RATIO = 2f/3f;
        bool hasBeganPlaying = false;


        GameSession gameSession;
        const int MAX_LIVES = 3;
        int
            level = 1,
            remainingLives = MAX_LIVES,
            totalKillCount = 0;


        public Form1()
        {
            InitializeComponent();

            //These allow the labels to have a transparent background: https://stackoverflow.com/a/53307487
            lblLargeText.Parent = PBgameArea;
            lblLargeText.BackColor = Color.Transparent;

            lblSmallText.Parent = PBgameArea;
            lblSmallText.BackColor = Color.Transparent;

            //Initialize and make visable the labels.
            lblLargeText.Text = "SPACE INVADERS";
            lblLargeText.Visible = true;
            lblSmallText.Text = "you get 3 lives\r\n\r\npress ENTER to begin";
            lblSmallText.Visible = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /**
             * This Form load event method initializes the Graphics graphics and Begins the gameSession.
             */

            KeyPreview = true;

            PBgameArea.Width = (int)(Size.Height * ASPECT_RATIO);
            PBgameArea.Height = Size.Height;
            PBgameArea.Location = new Point(Width / 2 - PBgameArea.Width / 2, 0);


            gameSession = new GameSession(DeviceDpi, PBgameArea, level, remainingLives);
            gameSession.Winner += gameSession_Victory;
            gameSession.GameOver += gameSession_GameOver;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /**
             * Koda Kozil: This keydown event method sets the appropriate gameSession and label properties.
             */

            if (e.KeyCode == Keys.A)
                gameSession.goLeft = true;
            else if (e.KeyCode == Keys.D)
                gameSession.goRight = true;
            
            if (e.KeyCode == Keys.Space)
                gameSession.doFire = true;

            if (e.KeyCode == Keys.Enter)
            {
                if(!hasBeganPlaying)
                {
                    //Begin gameSession
                    gameSession.Begin();

                    lblLargeText.Visible = false;
                    lblSmallText.Visible = false;

                    hasBeganPlaying = true;
                }
                else if (gameSession.SessionEnded)
                {
                    //Restart or Continue gameSession
                    gameSession = new GameSession(DeviceDpi, PBgameArea, level, remainingLives);
                    gameSession.Winner += gameSession_Victory;
                    gameSession.GameOver += gameSession_GameOver;
                    gameSession.Begin();

                    lblLargeText.Visible = false;
                    lblSmallText.Visible = false;
                }
                else
                {
                    //Pause or UnPause gameSession
                    if (gameSession.IsPaused())
                    {
                        gameSession.UnPause();
                        lblLargeText.Visible = false;
                        lblSmallText.Visible = false;
                    }
                    else
                    {
                        gameSession.Pause();
                        lblLargeText.Text = "PAUSED";
                        lblLargeText.Visible = true;
                        lblSmallText.Text = "press ENTER to resume";
                        lblSmallText.Visible = true;
                    }
                }
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            /**
             * This keydown event method sets the appropriate gameSession input properties to false.
             */

            if (e.KeyCode == Keys.A)
                gameSession.goLeft = false;
            else if (e.KeyCode == Keys.D)
                gameSession.goRight = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            /**
             * Koda Koziol: This pictureBox Paint event method (called when it's ready to rePaint) reDraws the pictureBox
             *              with all of the graphics in the gameSession.
             */

            if (hasBeganPlaying)
            {
                //Graphics g = ;
                gameSession.DrawEverything(e.Graphics);
            }
        }

        private void gameSession_Victory(object sender, GameSessionEndedEventArgs e)
        {
            /**
             * Koda Koziol: This gameSession Victory event method increments the level and restarts the gameSession when the Winner event is called.
             */

            totalKillCount += e.killCount;
            remainingLives = e.livesRemaining;

            gameSession.Pause();

            lblLargeText.Invoke((MethodInvoker)delegate //Learned this from stack overflow: https://stackoverflow.com/a/661662
            {
                lblLargeText.Text = "VICTORY";
                lblLargeText.Visible = true;
                lblSmallText.Text = remainingLives + " lives left\r\n\r\npress ENTER to continue";
                lblSmallText.Visible = true;
            });
            level++; //Makes it harder
        }
        private void gameSession_GameOver(object? sender, GameSessionEndedEventArgs e)
        {
            /**
             * Koda Koziol: This gameSession GameOver event method sets the level back to 1 and restarts the gameSession when the GameOver event is called.
             */
            totalKillCount += e.killCount;

            gameSession.Pause();

            lblLargeText.Invoke((MethodInvoker)delegate //Learned this from stack overflow: https://stackoverflow.com/a/661662
            {

                lblLargeText.Text = "GAME OVER";
                lblLargeText.Visible = true;
                lblSmallText.Text = totalKillCount + " invaders destroyed\r\n\r\npress ENTER to restart";
                lblSmallText.Visible = true;   
            });

            level = 1; //Start from the beginning
            totalKillCount = 0;
            remainingLives = MAX_LIVES;
        }
    }
}