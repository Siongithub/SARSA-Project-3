using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARSA_Project_3
{
    class Sarsa
    {
        public const int GRIDSIZE = 20;


        public const int FREE = 0;
        public const int BARRIER = -1;
        public const int REWARD = 1;


        public const int UP = 0;
        public const int RIGHT = 1;
        public const int DOWN = 2;
        public const int LEFT = 3;

        public const double INIT_Q_MIN = 0.01; //to initialize qTable to small, random, non-zero values
        public const double INIT_Q_MAX = 0.02; //between INIT_Q_MIN and INIT_Q_MAX. 



        private static Random random;              //pseudo-random number generator
        


        public int [][] gridWorld;  //row # 1st, then column #.  gridWorld[row][column]


        public double [][][] q; //q-Table

        public double[][] e;    //eligibility trace Table


        public Tuple<int, int> currLoc;


        public Sarsa ()
        {
            random = new Random();

            currLoc = new Tuple<int, int>(0,0);
      
            //// BEGIN init gridWorld ///////////////////////////////////////////
            gridWorld = new int [GRIDSIZE][];
            for (int i = 0; i < GRIDSIZE; i++)
            {
                gridWorld[i] = new int[GRIDSIZE];                
            }
            //add VALLEY OF DOOM
            for (int i = 3; i < 12; i++)
            {
                gridWorld[15][i] = BARRIER;
            }
            //add MOUNT SLUDGEMORE
            for (int i = 4; i < 8; i++)
            {
                for (int j = 14; j < 18; j++)
                {
                    gridWorld[i][j] = BARRIER;
                }
            }
            //add the ELL OF GRABBY GRANNIES
            for (int i = 3; i < 6; i++)
            { 
                gridWorld[i][5] = BARRIER;
                gridWorld[5][i] = BARRIER;
            }
            //add SHINY REWARD
            gridWorld[12][12] = REWARD;
            //// END init gridWorld /////////////////////////////////////////////

            q = new double[GRIDSIZE][][];
            for (int i = 0; i < GRIDSIZE; i++)
            {
                q[i] = new double[GRIDSIZE][];
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    q[i][j] = new double[4];   //up, down, left, right
                }
            }

            //initialize qTable to small, non-zero random values
            initQTable();

            //init eligibility Trace
            initETable();
        }

        public void initQTable()
        {
            //initialize qTable to small, non-zero random values
            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        q[i][j][k] = random.NextDouble() * (INIT_Q_MAX - INIT_Q_MIN) + INIT_Q_MIN;
                    }
                }
            }
        }
        
        public void initETable()
        {
            e = new double[GRIDSIZE][];
            for (int i = 0; i < GRIDSIZE; i++)
            {
                e[i] = new double[GRIDSIZE];
            }
        }


        public Tuple<int, int> GetStartLoc()
        {
            int row, col;

            do
            {
                row = random.Next(GRIDSIZE);
                col = random.Next(GRIDSIZE);
            }
            while (gridWorld[row][col] == BARRIER);

            Tuple<int, int> startLoc = new Tuple<int, int>(row, col);
            return startLoc;
        }

        public Tuple<int, int> GetRandomMove (Tuple<int, int> currentLoc)
        {
            int nextRow, nextCol;

            int direction = random.Next(4);  //up = 0; right = 1; down = 2; left = 3.
            
            switch (direction)
            {
                case 0:     //up
                    nextRow = currentLoc.Item1 - 1;
                    nextCol = currentLoc.Item2;
                    break;
                case 1:     //right
                    nextRow = currentLoc.Item1;
                    nextCol = currentLoc.Item2 + 1;
                    break;
                case 2:     //down
                    nextRow = currentLoc.Item1 + 1;
                    nextCol = currentLoc.Item2;
                    break;
                default:    //case 3, left.
                    nextRow = currentLoc.Item1;
                    nextCol = currentLoc.Item2 - 1;
                    break;
            }

            Tuple<int, int> nextLoc = new Tuple<int, int>(nextRow, nextCol);
            return nextLoc;
        }


        public Boolean CheckLocIsValid(Tuple<int, int> loc)
        {
            if ((loc.Item1 < 0) || (loc.Item1 >= GRIDSIZE) ||
                (loc.Item2 < 0) || (loc.Item2 >= GRIDSIZE) ||
                (gridWorld[loc.Item1][loc.Item2] == BARRIER))
            {
                Console.WriteLine("Bzzzt! Ran into barrier at row: " + loc.Item1 + ", col: " + loc.Item2);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
