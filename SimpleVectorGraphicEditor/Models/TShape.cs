using System;
using System.Drawing;

namespace SimpleVectorGraphicEditor
{
    public abstract class TShape
    {
        public abstract TPoint Origin { get; set; }

        public abstract int Width { get; }

        public abstract void Draw(Graphics graphics, Point origin);

        private static readonly Random rndColor = new Random(1);
        private static readonly Random rndWidth = new Random(2);

        private static readonly Color[] Colors =
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
}