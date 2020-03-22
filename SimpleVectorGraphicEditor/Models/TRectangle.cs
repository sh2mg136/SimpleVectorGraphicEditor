using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVectorGraphicEditor
{

    public class TRectangle : TShape
    {

        Point _start = new Point();
        Point _finish = new Point();

        Rectangle _rect;

        Color _color = Color.Black;
        int _width = 1;

        public TRectangle(Point start, Point finish)
        {
            _start = start;
            _finish = finish;
            _width = base.GetRandomWidth();
            _color = base.GetRandomColor();

            _rect = new Rectangle(_start, new Size(Math.Abs(_finish.X - _start.X), Math.Abs(_finish.Y - _start.Y)));
        }

        public override Point Origin
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public Point Finish
        {
            get
            {
                return _finish;
            }
            set
            {
                _finish = value;
            }
        }

        public override int Width => Math.Abs(_start.X - _finish.X);
        public int Height => Math.Abs(_start.Y - _finish.Y);

        public Rectangle Rect
        {
            get
            {
                var p1 = new Point(Math.Min(Origin.X, Finish.X), Math.Min(Origin.Y, Finish.Y));
                var p2 = new Point(Math.Max(Origin.X, Finish.X), Math.Max(Origin.Y, Finish.Y));
                return new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            }
        }


        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(Color.Gray, 1))
            {
                //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                //graphics.DrawRectangle(pen, RectCenter);
            }

            using (Pen pen = new Pen(_color, _width))
            {
                graphics.DrawRectangle(pen, Rect);
            }
        }

    }
}
