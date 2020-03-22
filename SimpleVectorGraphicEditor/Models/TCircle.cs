using System.Drawing;

namespace SimpleVectorGraphicEditor
{
    public class TCircle : TShape
    {
        private TPoint _origin = new TPoint();
        private Rectangle _rect;
        private Rectangle _rectCenter;
        private Color _color = Color.Black;
        private int _radius = 0;
        private int _width = 1;

        public TCircle(TPoint origin, int radius)
        {
            _origin = new TPoint(origin.P);
            _radius = radius;
            _width = base.GetRandomWidth();
            _color = base.GetRandomColor();

            _rect = new Rectangle(origin.X - radius, origin.Y - radius, radius * 2, radius * 2);

            //RectCenter = new Rectangle(origin.X - 2, origin.Y - 2, 4, 4);
        }

        public override TPoint Origin
        {
            get
            {
                return _origin;
            }
            set
            {
                _origin = value;
            }
        }

        public override int Width => _radius;

        public int Radius { get => _radius; set => _radius = value; }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(_origin.X - _radius, _origin.Y - _radius, _radius * 2, _radius * 2);
            }
        }

        // public Rectangle RectCenter { get => _rectCenter; set => _rectCenter = value; }

        public override void Draw(Graphics graphics, Point gridCenter)
        {
            Brush brush = Brushes.Gray;

            /*
            using (Pen pen = new Pen(Color.Gray, 1))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                graphics.DrawRectangle(pen, RectCenter);
            }
            */

            using (Pen pen = new Pen(_color, _width))
            {
                graphics.DrawEllipse(pen,
                    Origin.X - Radius + gridCenter.X,
                    -Origin.Y - Radius + gridCenter.Y,
                    Radius * 2,
                    Radius * 2);
            }
        }
    }
}