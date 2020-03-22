using System.Drawing;

namespace SimpleVectorGraphicEditor
{
    public class TGrid
    {
        private static readonly Pen pen = Pens.LightGray;
        private static Pen penAxe = new Pen(pen.Color, 3);

        private static Point p1 = new Point(0, 0);
        private static Point p2 = new Point(0, 0);

        private static Point GridCenter = new Point();

        static TGrid()
        {
            GridCenter = new Point(0, 0);
        }

        public static void SetCenter(int x, int y)
        {
            GridCenter.X = x;
            GridCenter.Y = y;
        }

        public static Point Origin => GridCenter;

        public static void Draw(Graphics graphics, int width, int height, int step)
        {
            // SetCenter(0, 0); //  width / 2; height / 2;

            graphics.DrawLine(penAxe, 0, GridCenter.Y, width, GridCenter.Y);
            graphics.DrawLine(penAxe, GridCenter.X, 0, GridCenter.X, height);

            p2.X = width;

            for (int i = GridCenter.Y; i <= height; i += step)
            {
                p1.Y = p2.Y = i;
                graphics.DrawLine(pen, p1, p2);
            }

            for (int i = GridCenter.Y; i >= -1 * height; i -= step)
            {
                p1.Y = p2.Y = i;
                graphics.DrawLine(pen, p1, p2);
            }

            /*
            for (int i = 1; i <= (int)(height / step); i++)
            {
                p1.Y = p2.Y = i * step;
                graphics.DrawLine(pen, p1, p2);
            }
            */

            p1.Y = 0;
            p2.Y = height;

            for (int i = GridCenter.X; i <= width; i += step)
            {
                p1.X = p2.X = i;
                graphics.DrawLine(pen, p1, p2);
            }

            for (int i = GridCenter.X; i >= -1 * width; i -= step)
            {
                p1.X = p2.X = i;
                graphics.DrawLine(pen, p1, p2);
            }

            /*
            for (int i = 1; i <= (int)(width / step); i++)
            {
                p1.X = p2.X = i * step;
                graphics.DrawLine(pen, p1, p2);
            }
            */

            p1.X = 1;
            p2.Y = 1;
            p2.X = width;
            p2.Y = 1;
            graphics.DrawLine(pen, p1, p2);

            p1.X = 1;
            p2.Y = 1;
            p2.X = 1;
            p2.Y = height;
            graphics.DrawLine(pen, p1, p2);
        }
    }
}