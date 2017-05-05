namespace SARSA_Project_3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RunOneEpisodeBtn = new System.Windows.Forms.Button();
            this.TestBtn = new System.Windows.Forms.Button();
            this.RunEpisodesBtn = new System.Windows.Forms.Button();
            this.TestDrawBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(19, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // RunOneEpisodeBtn
            // 
            this.RunOneEpisodeBtn.Location = new System.Drawing.Point(659, 188);
            this.RunOneEpisodeBtn.Name = "RunOneEpisodeBtn";
            this.RunOneEpisodeBtn.Size = new System.Drawing.Size(122, 72);
            this.RunOneEpisodeBtn.TabIndex = 6;
            this.RunOneEpisodeBtn.Text = "Run One Episode And Display Dora\'s Path";
            this.RunOneEpisodeBtn.UseVisualStyleBackColor = true;
            this.RunOneEpisodeBtn.Click += new System.EventHandler(this.RunOneEpisodeBtn_Click);
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(681, 44);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(75, 23);
            this.TestBtn.TabIndex = 7;
            this.TestBtn.Text = "TestBtn";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Visible = false;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // RunEpisodesBtn
            // 
            this.RunEpisodesBtn.Location = new System.Drawing.Point(659, 298);
            this.RunEpisodesBtn.Name = "RunEpisodesBtn";
            this.RunEpisodesBtn.Size = new System.Drawing.Size(122, 110);
            this.RunEpisodesBtn.TabIndex = 8;
            this.RunEpisodesBtn.Text = "Run 10000 Episodes";
            this.RunEpisodesBtn.UseVisualStyleBackColor = true;
            this.RunEpisodesBtn.Click += new System.EventHandler(this.RunEpisodesBtn_Click);
            // 
            // TestDrawBtn
            // 
            this.TestDrawBtn.Location = new System.Drawing.Point(666, 131);
            this.TestDrawBtn.Name = "TestDrawBtn";
            this.TestDrawBtn.Size = new System.Drawing.Size(115, 23);
            this.TestDrawBtn.TabIndex = 9;
            this.TestDrawBtn.Text = "Redraw Arrows";
            this.TestDrawBtn.UseVisualStyleBackColor = true;
            this.TestDrawBtn.Click += new System.EventHandler(this.TestDrawBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 732);
            this.Controls.Add(this.TestDrawBtn);
            this.Controls.Add(this.RunEpisodesBtn);
            this.Controls.Add(this.TestBtn);
            this.Controls.Add(this.RunOneEpisodeBtn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "SARSA learning with eligibility trace";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button RunOneEpisodeBtn;
        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.Button RunEpisodesBtn;
        private System.Windows.Forms.Button TestDrawBtn;
    }
}

