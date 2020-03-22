using System;
using System.Drawing;

namespace SimpleVectorGraphicEditor
{
    public class TLine : TShape
    {
        private TPoint _start = new TPoint();
        private TPoint _finish = new TPoint();
        private Color _color = Color.Black;
        private int _width = 1;

        public override TPoint Origin
        {
            get => _start; set => _start = value;
        }

        public override int Width => (int)Math.Sqrt(Math.Pow(Math.Abs(Origin.X - Finish.X), 2) + Math.Pow(Math.Abs(Origin.Y - Finish.Y), 2));

        public TPoint Finish { get => _finish; set => _finish = value; }

        public TLine(TPoint p1, TPoint p2)
        {
            _start = new TPoint(p1.P);
            _finish = new TPoint(p2.P);
            _width = base.GetRandomWidth();
            _color = base.GetRandomColor();
        }

        public override void Draw(Graphics graphics, Point gridCenter)
        {
            using (Pen pen = new Pen(_color, _width))
            {
                graphics.DrawLine(pen,
                    Origin.X + gridCenter.X,
                    -Origin.Y + gridCenter.Y,
                    Finish.X + gridCenter.X,
                    -Finish.Y + gridCenter.Y);
            }
        }
    }
}