using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SARSA_Project_3
{
    class Sarsa
    {
        public const int GRIDSIZE = 20;


        public const int FREE = 0;
        public const int PENALTY = -1;
        public const int REWARD = 1;


        public const int UP = 0;
        public const int RIGHT = 1;
        public const int DOWN = 2;
        public const int LEFT = 3;


        public const double INIT_Q_MIN = 0.01; //to initialize qTable to small, random, non-zero values
        public const double INIT_Q_MAX = 0.02; //between INIT_Q_MIN and INIT_Q_MAX. 


        public const double EPSILON_START = 0.9;  //90% exploration rate at start
        public const double EPSILON_END = .1;     //want to approach 10% exploration over time
        public const double EPSILON_INC = EPSILON_START - EPSILON_END;


        public const double ALPHA = 0.01;    //learning rate
        public const double GAMMA = 0.9;     //discount factor; importance of future rewards
        public const double LAMBDA = 0.9;    //decay rate for eligibility trace



        private static Random random;              //pseudo-random number generator

        public Form1 formObj;       //to read from, and draw to, the form.

        public int [][] gridWorld;  //row # 1st, then column #.  gridWorld[row][column]


        public double [][][] q; //q-Table

        public double[][][] e;    //eligibility trace Table

        public double explorationRate = EPSILON_START;  // exploration rate (epsilon)

        public int moveCounter;     //as we make more moves, exploration rate (epsilon) should go down.
        
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
                gridWorld[15][i] = PENALTY;
            }
            //add MOUNT SLUDGEMORE
            for (int i = 4; i < 8; i++)
            {
                for (int j = 14; j < 18; j++)
                {
                    gridWorld[i][j] = PENALTY;
                }
            }
            //add the ELL OF GRABBY GRANNIES
            for (int i = 3; i < 6; i++)
            { 
                gridWorld[i][5] = PENALTY;
                gridWorld[5][i] = PENALTY;
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
            e = new double[GRIDSIZE][][];
            for (int i = 0; i < GRIDSIZE; i++)
            {
                e[i] = new double[GRIDSIZE][];
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    e[i][j] = new double[4];   //up, down, left, right
                }
            }
        }

        //return a valid starting location.
        //starting location cannot be a PENALTY, or the reward.  
        //starting at the reward is pointless - nowhere to go.
        //can't update qTable or eTable without a direction
        public Tuple<int, int> GetStartLoc()
        {
            int row, col;

            do
            {
                row = random.Next(GRIDSIZE);
                col = random.Next(GRIDSIZE);
            }
            while ((gridWorld[row][col] == PENALTY) || (gridWorld[row][col] == REWARD));

            Tuple<int, int> startLoc = new Tuple<int, int>(row, col);
            return startLoc;
        }

        
        public Tuple<int, int> MoveInDirection(Tuple<int, int> locNow, int direction)
        {
            int nextRow, nextCol;

            switch (direction)
            {
                case UP:
                    nextRow = locNow.Item1 - 1;
                    nextCol = locNow.Item2;
                    break;
                case RIGHT:
                    nextRow = locNow.Item1;
                    nextCol = locNow.Item2 + 1;
                    break;
                case DOWN:
                    nextRow = locNow.Item1 + 1;
                    nextCol = locNow.Item2;
                    break;
                default:    //LEFT
                    nextRow = locNow.Item1;
                    nextCol = locNow.Item2 - 1;
                    break;
            }
            return new Tuple<int, int>(nextRow, nextCol);
        }



        public Boolean CheckLocIsValid(Tuple<int, int> locNow)
        {
            if ((locNow.Item1 < 0) || (locNow.Item1 >= GRIDSIZE) ||
                (locNow.Item2 < 0) || (locNow.Item2 >= GRIDSIZE) ||
                (gridWorld[locNow.Item1][locNow.Item2] == PENALTY))
            {
                //Debug.WriteLine("Bzzzt! Ran into PENALTY at row: " + locNow.Item1 + ", col: " + locNow.Item2);
                return false;
            }
            else
            {
                return true;
            }
        }

        /*
        public void MoveOne()
        {
            Tuple<int, int> nextLoc;

            int direction = random.Next(4);  //up = 0; right = 1; down = 2; left = 3.

            nextLoc = MoveInDirection(currLoc, direction);
            if (CheckLocIsValid(nextLoc))
            {
                currLoc = nextLoc;
                formObj.DrawGrid(gridWorld);
                formObj.DrawSquare(currLoc);
            }
        }
        */


        //get the exploration rate
        public double calcExplorationRate(int counter)
        {
            double result;
            if (counter > 10000) //dont bother calculating for counter > 10000; just use EPSILON_END.
            {
                result = EPSILON_END;
            }
            else if (counter % 100 == 0)     //only recalc once every 100 moves
            {
                int exponent = (counter / 100);
                //this will return EPSILON_START at first, and decays to EPSILON_END over time.
                result = Math.Pow(EPSILON_INC, exponent + 1) + EPSILON_END;
            }
            else
            {
                result = explorationRate;
            }
            return result;
        }

        public int getBestDirection(int yVal, int xVal)
        {
            int bestDirection = -1;
            double? maxVal = null;

            //check UP, RIGHT, DOWN and LEFT.  which is best?
            for (int i = UP; i <= LEFT; i++)
            {
                if ((maxVal == null) || (q[yVal][xVal][i] > maxVal))
                {
                        maxVal = q[yVal][xVal][i];
                        bestDirection = i;
                }
            }
            return bestDirection;
        }

        public void RunMultipleEpisodes()
        {
            for (int i = 0; i < 10000; i++)
            {
                RunOneEpisode(false);
                if (i % 100 == 0)
                {
                    Console.WriteLine("Running 100 episodes starting at iteration " + i + "...");
                }
            }
            Console.WriteLine("Done!");
        }


        public void RunOneEpisode(Boolean drawPath)
        {
            Boolean done = false;
            Tuple<int, int> startLoc = null;
            Tuple<int, int> nextLoc = null;
            List<int> pathTaken = new List<int>();
            int direction;
            int direction2;
            double r;
            double delta;

            //init eligibility Trace
            initETable();

            //place Dora the Explorer at the starting position
            startLoc = GetStartLoc();
            currLoc = startLoc;

            if (drawPath)
            {
                formObj.DrawGrid(gridWorld);
                formObj.DrawSquare(currLoc);
            }

            while (!done)
            {
                explorationRate = calcExplorationRate(moveCounter);
                double rand = random.NextDouble();

                if (rand < explorationRate)
                {
                    //explore
                    direction = random.Next(4);  //UP = 0; RIGHT = 1; DOWN = 2; LEFT = 3
                }
                else
                {
                    //exploit
                    direction = getBestDirection(currLoc.Item1, currLoc.Item2);
                }

                pathTaken.Add(direction);
                nextLoc = MoveInDirection(currLoc, direction);

                if (!CheckLocIsValid(nextLoc))
                {
                    done = true;
                    r = PENALTY;
                }
                else if (gridWorld[nextLoc.Item1][nextLoc.Item2] == REWARD)
                {
                    done = true;
                    r = REWARD;
                }
                else
                {
                    r = 0;
                }

                //if we hit a barrier, or reached reward, Q(s',a') = 0.
                if (done)
                {
                    delta = r - q[currLoc.Item1][currLoc.Item2][direction];
                }
                else
                {
                    direction2 = getBestDirection(nextLoc.Item1, nextLoc.Item2);

                    delta = r + GAMMA * q[nextLoc.Item1][nextLoc.Item2][direction2] - 
                            q[currLoc.Item1][currLoc.Item2][direction];
                }

                e[currLoc.Item1][currLoc.Item2][direction] += 1;

                for (int i = 0; i < GRIDSIZE; i++)
                {
                    for (int j = 0; j < GRIDSIZE; j++)
                    {
                        for (int k = UP; k <= LEFT; k++)
                        {
                            if (e[i][j][k] != 0)
                            {
                                q[i][j][k] += ALPHA * delta * e[i][j][k];
                                e[i][j][k] *= GAMMA * LAMBDA;
                            }
                        }
                    }
                }

                currLoc = nextLoc;
                moveCounter++;
            }

            if (drawPath)
            {
                //draw path taken
                formObj.DrawPathTaken(startLoc, pathTaken);
            }
        }
    }
}
