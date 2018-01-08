using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVectorGraphicEditor
{

    public abstract class TShape
    {
        public abstract Point Origin { get; set; }

        public abstract int Width { get; }

        public abstract void Draw(Graphics graphics);

        static readonly Random rndColor = new Random(1);
        static readonly Random rndWidth = new Random(2);

        static readonly Color[] Colors =
            {
                Color.Black,
                Color.White,
                Color.Red,
                Color.Pink,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.LightPink,
                Color.Magenta,
                Color.Pink,
                Color.Cyan,
            };

        public Color GetRandomColor()
        {
            rndColor.Next(0, Colors.Length * 2);
            return Colors[rndColor.Next(0, Colors.Length)];
        }

        public int GetRandomWidth()
        {
            rndWidth.Next(2, 50);
            return rndWidth.Next(2, 20);
        }

    }


    public class Grid
    {
        static readonly Pen pen = Pens.LightGray;
        static readonly int step = 20;

        public static void Draw(Graphics graphics, int width, int height)
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(width, 0);
            
            for (int i = 1; i <= (int)(height / step); i++)
            {
                p1.Y = p2.Y = i * step;
                graphics.DrawLine(pen, p1, p2);
            }

            p1.Y = 0;
            p2.Y = height;

            for (int i = 1; i <= (int)(width / step); i++)
            {
                p1.X = p2.X = i * step;
                graphics.DrawLine(pen, p1, p2);
            }

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
