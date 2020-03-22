using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVectorGraphicEditor
{
    public class TLine : TShape
    {
        Point _start = new Point();
        Point _finish = new Point();
        Color _color = Color.Black;
        int _width = 1;


        public override Point Origin
        {
            get => _start; set => _start = value;
        }

        public override int Width => (int)Math.Sqrt(Math.Pow(Math.Abs(Origin.X - Finish.X), 2) + Math.Pow(Math.Abs(Origin.Y - Finish.Y), 2));

        public Point Finish { get => _finish; set => _finish = value; }

        public TLine(Point p1, Point p2)
        {
            _start = p1;
            _finish = p2;
            _width = base.GetRandomWidth();
            _color = base.GetRandomColor();
        }

        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(_color, _width))
            {
                graphics.DrawLine(pen, Origin, Finish);
            }
        }

    }

}
