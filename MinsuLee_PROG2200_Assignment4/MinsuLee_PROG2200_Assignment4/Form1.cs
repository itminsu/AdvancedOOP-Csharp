using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinsuLee_PROG2200_Assignment4
{
    public partial class gameForm : Form
    {
        //Random for goal
        Random random = new Random();

        MazeRunner runner; 
        Goal goal;

        //The Cells
        private Cells[,] arrayofCells = new Cells[20, 20];

        //Array used for getting the maze.
        private char[,] inputFileArray = new char[41, 41];

        int level = 1;

        public gameForm()
        {
            InitializeComponent();

            runner = new MazeRunner();

            CreateCells();
            LoadMaze();
            BuildBorders();

            generateGoal();

            labelMessage.Hide();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void gameForm_Paint(object sender, PaintEventArgs e)
        {
            // Draw runner
            runner.Draw(e.Graphics);
            // Draw goal
            goal.Draw(e.Graphics);
            // Draw Maze
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    arrayofCells[x, y].DrawCells(e.Graphics);
                }
            }
        }

        private void gameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                //Left Arrow Key
                case Keys.Left:
                    {
                        if (!CheckLeftWall(runner.CurrentXPos, runner.CurrentYPos))
                        {
                            runner.Move("left");
                            CheckGoal(runner.CurrentXPos, runner.CurrentYPos);
                        }

                        break;
                    }

                //Right Arrow Key
                case Keys.Right:
                    {
                        if (!CheckRightWall(runner.CurrentXPos, runner.CurrentYPos))
                        {
                            runner.Move("right");
                            CheckGoal(runner.CurrentXPos, runner.CurrentYPos);
                        }

                        break;
                    }

                //Up Arrow Key
                case Keys.Up:
                    {
                        if (!CheckTopWall(runner.CurrentXPos, runner.CurrentYPos))
                        {
                            runner.Move("up");
                            CheckGoal(runner.CurrentXPos, runner.CurrentYPos);
                        }

                        break;
                    }

                //Down Arrow Key
                case Keys.Down:
                    {
                        if (!CheckBottomWall(runner.CurrentXPos, runner.CurrentYPos))
                        {
                            runner.Move("down");
                            CheckGoal(runner.CurrentXPos, runner.CurrentYPos);
                        }

                        break;
                    }
                case Keys.Space:
                    {
                        //start or stop the timer
                        if (timer1.Enabled)
                        {
                            timer1.Stop();
                        }
                        else
                        {
                            timer1.Start();
                        }
                        break;
                    }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Create 20 x 20 cells
        /// </summary>
        public void CreateCells()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    arrayofCells[x, y] = new Cells(x, y);
                }
            }
        }

        /// <summary>
        /// Load maze text files by level
        /// </summary>
        public void LoadMaze()
        {
            using (StreamReader streamReader = new StreamReader(string.Format("Resources/maze{0}.txt", level)))
            {
                string line;
                int lineCount = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    for (int x = 0; x < 41; x++)
                    {
                        inputFileArray[lineCount, x] = line[x];
                    }

                    lineCount++;

                }
            }
        }

        /// <summary>
        /// Generate borders
        /// </summary>
        public void BuildBorders()
        {
            int x;
            int y;

            int mazeX = 0;
            int mazeY = 0;

            for (x = 1; x < 41; x += 2)
            {
                for (y = 1; y < 41; y += 2)
                {

                    //Check Left.
                    if (inputFileArray[x, y - 1] != ' ')
                    {
                        arrayofCells[mazeX, mazeY].LeftBorder = true;
                    }
                    //Check Right.
                    if (inputFileArray[x, y + 1] != ' ')
                    {
                        arrayofCells[mazeX, mazeY].RightBorder = true;
                    }
                    //Check Top.
                    if (inputFileArray[x - 1, y] != ' ')
                    {
                        arrayofCells[mazeX, mazeY].TopBorder = true;
                    }
                    //Check Bottom.
                    if (inputFileArray[x + 1, y] != ' ')
                    {
                        arrayofCells[mazeX, mazeY].BottomBorder = true;
                    }

                    mazeY++;

                }

                mazeY = 0;
                mazeX++;
            }
        }

        /// <summary>
        /// Generate goal on random location.
        /// </summary>
        public void generateGoal()
        {
            int x = random.Next(0, 20);
            int y = random.Next(0, 20);

            goal = new Goal(x, y);
        }

        /// <summary>
        /// Check goal and pop up message.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckGoal(int x, int y)
        {
            if (goal.xCord == x && goal.yCord == y)
            {
                level += 1;
                if(level > 5)
                {
                    level = 1;
                }
                generateGoal();
                //LoadMaze();
                timer1.Stop();
                labelMessage.Show();
                timer2.Interval = 5000;
                timer2.Start();
                
                return true;
            }

            return false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            labelMessage.Hide();
        }

        /// <summary>
        /// Checks the Left Border of the Cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckLeftWall(int x, int y)
        {
            if (x < -1)
            {
                return true;
            }
            return arrayofCells[y, x].LeftBorder;

        }

        /// <summary>
        /// Checks the Right Border of the Cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckRightWall(int x, int y)
        {
            if (x > 19)
            {
                return true;
            }
            return arrayofCells[y, x].RightBorder;
        }

        /// <summary>
        /// Checks the Top Border of the Cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckTopWall(int x, int y)
        {
            if (y < -1)
            {
                return true;
            }
            return arrayofCells[y, x].TopBorder;
        }

        /// <summary>
        /// Checks the Bottom Border of the Cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckBottomWall(int x, int y)
        {
            if (y > 19)
            {
                return true;
            }
            return arrayofCells[y, x].BottomBorder;
        }
    }
}
