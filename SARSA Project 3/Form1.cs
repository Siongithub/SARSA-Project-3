using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;


namespace SARSA_Project_3
{
    public partial class Form1 : Form
    {
        Sarsa sarsa;

        public Form1()
        {
            InitializeComponent();

            sarsa = new Sarsa();
            sarsa.formObj = this;

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

            SolidBrush penaltyBrush = new SolidBrush(Color.Black);
            SolidBrush rewardBrush = new SolidBrush(Color.Orange);
            for (int i = 0; i < grid.Length; i++)   //rows
            {
                for (int j = 0; j < grid[0].Length; j++)    //columns
                {
                    // 0 is fully transparent, and 255 is fully opaque
                    //grayScale = Convert.ToInt32((digitArray[i * 8 + j] / (double)16) * 255);
                    //grayScaleColor = Color.FromArgb(grayScale, Color.Black);

                    if (grid[i][j] == Sarsa.PENALTY)
                    {
                        gridGraphics.FillRectangle(penaltyBrush, j * square, i * square, square, square);
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

            SolidBrush doraBrush = new SolidBrush(Color.LightBlue);

            gridGraphics.FillRectangle(doraBrush, loc.Item2 * square + 1, loc.Item1 * square + 1, square - 1, square - 1);

            pictureBox1.Image = gridBmp;
        }

        public void DrawDirections()
        {
            DrawGrid(sarsa.gridWorld);

            int direction;

            double min = 0;
            double max = 0;
            

            for (int i = 0; i < sarsa.q.Length; i++)
            {
                for (int j = 0; j < sarsa.q[0].Length; j++)
                {
                    direction = sarsa.getBestDirection(i, j);

                    if (sarsa.q[i][j][direction] > max)
                    {
                        max = sarsa.q[i][j][direction];
                    }
                    else if (sarsa.q[i][j][direction] < min)
                    {
                        min = sarsa.q[i][j][direction];
                    }
                    
                }
            }



            Console.WriteLine("qTable Min: " + min);
            Console.WriteLine("qTable Max: " + max);

           
            Bitmap gridBmp = (Bitmap)pictureBox1.Image;
            Graphics gridGraphics = Graphics.FromImage(gridBmp);
            

            double scaled;
            int arrowHeadSize;
            int arrowLength;

            int xStart;
            int yStart;
            int xEnd;
            int yEnd;


            int imageSize = pictureBox1.Size.Width;
            int square = imageSize / Sarsa.GRIDSIZE;
            int fullArrowLength = square;
            int fullArrowCap = (int)(square/(double)4);

            Pen p = new Pen(Color.Blue, 1);

            for (int i = 0; i < sarsa.q.Length; i++)
            {
                for (int j = 0; j < sarsa.q[0].Length; j++)
                {
                    if ((sarsa.gridWorld[i][j] != Sarsa.PENALTY) && (sarsa.gridWorld[i][j] != Sarsa.REWARD))
                    {
                        direction = sarsa.getBestDirection(i, j);

                        scaled = (sarsa.q[i][j][direction] - min) / (max - min);  //0 to 1.

                        arrowHeadSize = (int)Math.Ceiling(scaled * fullArrowCap);
                        arrowLength = (int)Math.Ceiling(scaled * fullArrowLength);

                        switch (direction)
                        {
                            case Sarsa.UP:
                                xStart = j * square + square / 2;
                                xEnd = xStart;

                                yEnd = i * square;
                                yStart = i * square + arrowLength;
                                break;
                            case Sarsa.RIGHT:
                                xStart = j * square;
                                xEnd = j * square + arrowLength;

                                yStart = i * square + square / 2;
                                yEnd = yStart;
                                break;
                            case Sarsa.DOWN:
                                xStart = j * square + square / 2;
                                xEnd = xStart;

                                yStart = i * square;
                                yEnd = i * square + arrowLength;
                                break;
                            case Sarsa.LEFT:
                                xStart = j * square + arrowLength;
                                xEnd = j * square;

                                yStart = i * square + square / 2;
                                yEnd = yStart;
                                break;
                            default:
                                Debug.WriteLine("Error.  Invalid direction: " + direction);
                                xStart = 100;
                                yStart = 100;
                                yEnd = 0;
                                xEnd = 0;
                                break;
                        }

                        AdjustableArrowCap bigArrow = new AdjustableArrowCap(arrowHeadSize, arrowHeadSize);
                        p.CustomEndCap = bigArrow;
                        gridGraphics.DrawLine(p, xStart, yStart, xEnd, yEnd);
                    }
                }
            }


            pictureBox1.Image = gridBmp;


        }


        public void DrawPathTaken(Tuple<int, int> startLoc, List<int> path)
        {
            int imageSize = pictureBox1.Size.Width;
            int square = imageSize / Sarsa.GRIDSIZE;
            int yStart = startLoc.Item1 * square + square/2;
            int xStart = startLoc.Item2 * square + square/2;
            int yEnd;
            int xEnd;

            Bitmap gridBmp = (Bitmap)pictureBox1.Image;
            Graphics gridGraphics = Graphics.FromImage(gridBmp);

            Pen grayPen = new Pen(Color.Gray, 1);

            foreach (int dir in path)
            {
                switch (dir)
                {
                    case Sarsa.UP:
                        yEnd = yStart - square;
                        xEnd = xStart;
                        break;
                    case Sarsa.RIGHT:
                        yEnd = yStart;
                        xEnd = xStart + square;
                        break;
                    case Sarsa.DOWN:
                        yEnd = yStart + square;
                        xEnd = xStart;
                        break;
                    case Sarsa.LEFT:
                        yEnd = yStart;
                        xEnd = xStart - square;
                        break;
                    default:
                        Debug.WriteLine("Error.  Invalid direction: " + dir);
                        yEnd = 0;
                        xEnd = 0;
                        break;
                }
                gridGraphics.DrawLine(grayPen, xStart, yStart, xEnd, yEnd);

                yStart = yEnd;
                xStart = xEnd;
            }                      

            pictureBox1.Image = gridBmp;
        }

        /*
        private void FindGoldBtn_Click(object sender, EventArgs e)
        {
            //Tuple<int, int> startLoc = sarsa.GetStartLoc();
            sarsa.currLoc = sarsa.GetStartLoc();

            Console.WriteLine("row: " + sarsa.currLoc.Item1 + ", col: " + sarsa.currLoc.Item2);

            DrawGrid(sarsa.gridWorld);
            DrawSquare(sarsa.currLoc);
        }
        */

        private void RunOneEpisodeBtn_Click(object sender, EventArgs e)
        {
            sarsa.RunOneEpisode(true);
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            double result;
            result = sarsa.calcExplorationRate(0);
            Console.WriteLine("counter 0: " + result);

            result = sarsa.calcExplorationRate(10);
            Console.WriteLine("counter 10: " + result);

            result = sarsa.calcExplorationRate(100);
            Console.WriteLine("counter 100: " + result);

            result = sarsa.calcExplorationRate(1000);
            Console.WriteLine("counter 1000: " + result);

            result = sarsa.calcExplorationRate(10000);
            Console.WriteLine("counter 10000: " + result);
        }

        private void RunEpisodesBtn_Click(object sender, EventArgs e)
        {
            sarsa.RunMultipleEpisodes();
            DrawDirections();
        }

        private void TestDrawBtn_Click(object sender, EventArgs e)
        {
            DrawDirections();
        }
    }
}
