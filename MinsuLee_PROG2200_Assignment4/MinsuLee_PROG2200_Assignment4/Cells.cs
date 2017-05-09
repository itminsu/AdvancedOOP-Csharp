using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinsuLee_PROG2200_Assignment4
{
    class Cells
    {
        //Cells Location
        private int x;
        private int y;
        
        public Cells(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        // Size of each cell
        private const int CELL_SIZE = 30;

        //The border of Each Cell. LEFT, RIGHT, TOP, BOTTOM
        private bool[] Borders = new bool[4] { false, false, false, false };
     
        /// <summary>
        /// Draw cells
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawCells(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(brush);

            //Left
            if (Borders[0] == true)
            {
                graphics.DrawLine(pen,
                    y * CELL_SIZE, x * CELL_SIZE,
                        (y) * CELL_SIZE, (x + 1) * CELL_SIZE);
            }

            //Right
            if (Borders[1] == true)
            {
                graphics.DrawLine(pen,
                    (y + 1) * CELL_SIZE, x * CELL_SIZE,
                        (y + 1) * CELL_SIZE, (x + 1) * CELL_SIZE);
            }

            //Top 
            if (Borders[2] == true)
            {
                graphics.DrawLine(pen,
                    y * CELL_SIZE, x * CELL_SIZE,
                        (y + 1) * CELL_SIZE, x * CELL_SIZE);
            }

            //Bottom 
            if (Borders[3] == true)
            {
                graphics.DrawLine(pen,
                    y * CELL_SIZE, (x + 1) * CELL_SIZE,
                        (y + 1) * CELL_SIZE, (x + 1) * CELL_SIZE);
            }  
        }
        
        public bool LeftBorder
        {
            get { return Borders[0]; }
            set { Borders[0] = value; }
        }

        public bool RightBorder
        {
            get { return Borders[1]; }
            set { Borders[1] = value; }
        }

        public bool TopBorder
        {
            get { return Borders[2]; }
            set { Borders[2] = value; }
        }

        public bool BottomBorder
        {
            get { return Borders[3]; }
            set { Borders[3] = value; }
        }
    }
}
