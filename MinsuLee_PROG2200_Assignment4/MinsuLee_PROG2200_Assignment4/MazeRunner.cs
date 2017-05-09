using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinsuLee_PROG2200_Assignment4
{
    public class MazeRunner
    {
        //Current Location.
        private int x;
        private int y;

        private int height;
        private int width;

        private string currentDirrection;

        private const int PIXELSIZE = 30;

        private Rectangle displayArea;
        private Point startPos;
        
        Image image = Image.FromFile("Resources/packman.png");
        public MazeRunner()
        {

            width = PIXELSIZE;
            height = PIXELSIZE;

            displayArea.Width = width;
            displayArea.Height = height;

            startPos = new Point(0, 0);

            displayArea = new Rectangle(startPos, new Size(width, height));

        }

        /// <summary>
        /// Draw runner character
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            image = Image.FromFile("Resources/packman.png");
            switch (currentDirrection)
            {

                case "left":
                    {
                        image.RotateFlip(RotateFlipType.Rotate180FlipY);
                        break;
                    }

                case "up":
                    {
                        image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    }

                case "down":
                    {
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    }

                default:
                    {
                        break;
                    }

            }

            graphics.DrawImage(image, displayArea);

        }

        /// <summary>
        /// Handle the Moving of the Maze Character by direction.
        /// </summary>
        /// <param name="direction"></param>
        public void Move(string direction)
        {
            switch (direction)
            {

                //Key left was pressed.
                case "left":
                    {
                        displayArea.X -= PIXELSIZE;
                        x--;

                        currentDirrection = direction;

                        break;
                    }

                //Key right was pressed.
                case "right":
                    {
                        displayArea.X += PIXELSIZE;
                        x++;

                        currentDirrection = direction;

                        break;
                    }

                //Key up was pressed.
                case "up":
                    {
                        displayArea.Y -= PIXELSIZE;
                        y--;

                        currentDirrection = direction;

                        break;
                    }

                //Key down was pressed.
                case "down":
                    {
                        displayArea.Y += PIXELSIZE;
                        y++;

                        currentDirrection = direction;

                        break;
                    }

            }
        }

        public int CurrentXPos
        {
            get { return x; }
            set { x = value; }
        }

        public int CurrentYPos
        {
            get { return y; }
            set { y = value; }
        }
    }
}
