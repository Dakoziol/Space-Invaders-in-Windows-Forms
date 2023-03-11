namespace KodaKoziol2263Ex9B
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PBgameArea = new System.Windows.Forms.PictureBox();
            this.lblLargeText = new System.Windows.Forms.Label();
            this.lblSmallText = new System.Windows.Forms.Label();
            this.txtControlHints = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBgameArea)).BeginInit();
            this.SuspendLayout();
            // 
            // PBgameArea
            // 
            this.PBgameArea.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PBgameArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(32)))));
            this.PBgameArea.Location = new System.Drawing.Point(88, 0);
            this.PBgameArea.Margin = new System.Windows.Forms.Padding(1);
            this.PBgameArea.Name = "PBgameArea";
            this.PBgameArea.Size = new System.Drawing.Size(280, 365);
            this.PBgameArea.TabIndex = 1;
            this.PBgameArea.TabStop = false;
            this.PBgameArea.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // lblLargeText
            // 
            this.lblLargeText.BackColor = System.Drawing.Color.Black;
            this.lblLargeText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLargeText.Font = new System.Drawing.Font("Impact", 0.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Inch);
            this.lblLargeText.ForeColor = System.Drawing.Color.IndianRed;
            this.lblLargeText.Location = new System.Drawing.Point(0, 0);
            this.lblLargeText.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblLargeText.Name = "lblLargeText";
            this.lblLargeText.Size = new System.Drawing.Size(448, 273);
            this.lblLargeText.TabIndex = 2;
            this.lblLargeText.Text = "LARGE TEXT";
            this.lblLargeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSmallText
            // 
            this.lblSmallText.BackColor = System.Drawing.Color.Black;
            this.lblSmallText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSmallText.Font = new System.Drawing.Font("OCR A Extended", 0.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Inch);
            this.lblSmallText.ForeColor = System.Drawing.Color.IndianRed;
            this.lblSmallText.Location = new System.Drawing.Point(0, 26);
            this.lblSmallText.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblSmallText.Name = "lblSmallText";
            this.lblSmallText.Size = new System.Drawing.Size(448, 256);
            this.lblSmallText.TabIndex = 3;
            this.lblSmallText.Text = "small TEXT";
            this.lblSmallText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtControlHints
            // 
            this.txtControlHints.BackColor = System.Drawing.Color.Black;
            this.txtControlHints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtControlHints.Enabled = false;
            this.txtControlHints.ForeColor = System.Drawing.Color.LightGray;
            this.txtControlHints.HideSelection = false;
            this.txtControlHints.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtControlHints.Location = new System.Drawing.Point(0, 0);
            this.txtControlHints.Margin = new System.Windows.Forms.Padding(1);
            this.txtControlHints.Multiline = true;
            this.txtControlHints.Name = "txtControlHints";
            this.txtControlHints.ReadOnly = true;
            this.txtControlHints.ShortcutsEnabled = false;
            this.txtControlHints.Size = new System.Drawing.Size(105, 73);
            this.txtControlHints.TabIndex = 4;
            this.txtControlHints.Text = "Left: A\r\nRight: D\r\nFire: SPACE\r\nPause: ENTER";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(448, 282);
            this.Controls.Add(this.txtControlHints);
            this.Controls.Add(this.lblSmallText);
            this.Controls.Add(this.lblLargeText);
            this.Controls.Add(this.PBgameArea);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.PBgameArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private PictureBox PBgameArea;
        private Label lblLargeText;
        private Label lblSmallText;
        private TextBox txtControlHints;
    }
}