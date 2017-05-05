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
            this.FindGoldBtn = new System.Windows.Forms.Button();
            this.MoveOneBtn = new System.Windows.Forms.Button();
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
            // FindGoldBtn
            // 
            this.FindGoldBtn.Location = new System.Drawing.Point(779, 55);
            this.FindGoldBtn.Name = "FindGoldBtn";
            this.FindGoldBtn.Size = new System.Drawing.Size(75, 23);
            this.FindGoldBtn.TabIndex = 4;
            this.FindGoldBtn.Text = "Find Gold";
            this.FindGoldBtn.UseVisualStyleBackColor = true;
            this.FindGoldBtn.Click += new System.EventHandler(this.FindGoldBtn_Click);
            // 
            // MoveOneBtn
            // 
            this.MoveOneBtn.Location = new System.Drawing.Point(779, 134);
            this.MoveOneBtn.Name = "MoveOneBtn";
            this.MoveOneBtn.Size = new System.Drawing.Size(75, 23);
            this.MoveOneBtn.TabIndex = 5;
            this.MoveOneBtn.Text = "One Move";
            this.MoveOneBtn.UseVisualStyleBackColor = true;
            this.MoveOneBtn.Click += new System.EventHandler(this.MoveOneBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 732);
            this.Controls.Add(this.MoveOneBtn);
            this.Controls.Add(this.FindGoldBtn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button FindGoldBtn;
        private System.Windows.Forms.Button MoveOneBtn;
    }
}

