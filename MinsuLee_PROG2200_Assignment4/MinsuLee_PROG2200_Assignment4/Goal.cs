using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinsuLee_PROG2200_Assignment4
{
    class Goal
    {
        private int x;
        private int y;

        private Rectangle displayArea;

        private int height;
        private int width;

        private const int PIXELSIZE = 30;

        public Goal(int x, int y)
        {
            this.x = x;
            this.y = y;

            displayArea = new Rectangle(new Point(x * 30, y * 30), new Size(PIXELSIZE, PIXELSIZE));
        }

        /// <summary>
        /// Draw goal
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.Red);
            graphics.FillEllipse(brush, displayArea);

        }

        public int xCord
        {
            get { return x; }
        }
        public int yCord
        {
            get { return y; }
        }
    }
}
