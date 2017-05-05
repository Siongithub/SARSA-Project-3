using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SARSA_Project_3
{
    public partial class Form1 : Form
    {
        Sarsa sarsa;

        public Form1()
        {
            InitializeComponent();
            sarsa = new Sarsa();
            DrawGrid(sarsa.gridWorld);
        }

        public void DrawGrid(int[][] grid)
        {
            int imageSize = pictureBox1.Size.Width;
            int square = imageSize / grid.Length;

            Bitmap gridBmp = new Bitmap(imageSize, imageSize);
            Graphics gridGraphics = Graphics.FromImage(gridBmp);

            Pen blackPen = new Pen(Color.Black, 1);
            //draw horizontal and vertical gridlines
            for (int i = 1; i < grid.Length; i++)
            {
                // Draw horizontal lines to screen.
                gridGraphics.DrawLine(blackPen, 0, i * square, imageSize, i * square);

                // Draw vertical lines to screen.
                gridGraphics.DrawLine(blackPen, i * square, 0, i * square, imageSize);
            }

            SolidBrush barrierBrush = new SolidBrush(Color.Black);
            SolidBrush rewardBrush = new SolidBrush(Color.Orange);
            for (int i = 0; i < grid.Length; i++)   //rows
            {
                for (int j = 0; j < grid[0].Length; j++)    //columns
                {
                    // 0 is fully transparent, and 255 is fully opaque
                    //grayScale = Convert.ToInt32((digitArray[i * 8 + j] / (double)16) * 255);
                    //grayScaleColor = Color.FromArgb(grayScale, Color.Black);

                    if (grid[i][j] == Sarsa.BARRIER)
                    {
                        gridGraphics.FillRectangle(barrierBrush, j * square, i * square, square, square);
                    }
                    else if (grid[i][j] == Sarsa.REWARD)
                    {
                        gridGraphics.FillRectangle(rewardBrush, j * square + 1, i * square + 1, square -1, square - 1);
                    }
                }

            }
            pictureBox1.Image = gridBmp;
        }

        public void DrawSquare(Tuple<int, int> loc)
        {
            int imageSize = pictureBox1.Size.Width;
            int square = imageSize / Sarsa.GRIDSIZE;

            Bitmap gridBmp = (Bitmap) pictureBox1.Image;
            Graphics gridGraphics = Graphics.FromImage(gridBmp);

            SolidBrush doraBrush = new SolidBrush(Color.Blue);

            gridGraphics.FillRectangle(doraBrush, loc.Item2 * square + 1, loc.Item1 * square + 1, square - 1, square - 1);

            pictureBox1.Image = gridBmp;
        }


        private void FindGoldBtn_Click(object sender, EventArgs e)
        {


            //Tuple<int, int> startLoc = sarsa.GetStartLoc();
            sarsa.currLoc = sarsa.GetStartLoc();

            Console.WriteLine("row: " + sarsa.currLoc.Item1 + ", col: " + sarsa.currLoc.Item2);

            DrawGrid(sarsa.gridWorld);
            DrawSquare(sarsa.currLoc);
        }

        private void MoveOneBtn_Click(object sender, EventArgs e)
        {
            Tuple<int, int> nextLoc;

            nextLoc = sarsa.GetRandomMove(sarsa.currLoc);
            if (sarsa.CheckLocIsValid(nextLoc))
            {
                sarsa.currLoc = nextLoc;
                DrawGrid(sarsa.gridWorld);
                DrawSquare(sarsa.currLoc);
            }
        }
    }
}
