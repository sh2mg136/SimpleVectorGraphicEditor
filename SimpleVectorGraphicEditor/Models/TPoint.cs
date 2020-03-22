using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVectorGraphicEditor
{
    public class TPoint : TShape
    {
        Point _origin = new Point();
        Pen _pen = Pens.Lime;

        public TPoint()
        {
            _origin.X = 0;
            _origin.Y = 0;
        }

        public TPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public TPoint(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }


        public override TPoint Origin
        {
            get => this;
            set
            {
            }
        }

        public override int Width => 4;

        public int X
        {
            get => _origin.X;
            set => _origin.X = value;
        }

        public int Y
        {
            get => _origin.Y;
            set => _origin.Y = value;
        }

        public TPoint ToUserCoords(Point gridCenter)
        {
            this.X -= gridCenter.X;
            this.Y -= gridCenter.Y;
            return this;
            // return new Point(this.OriginX - gridCenter.X, this.OriginY - gridCenter.Y);
        }

        public TPoint ToUserCoords(Point e, Point gridCenter)
        {
            this.X = e.X - gridCenter.X;
            this.Y = -e.Y + gridCenter.Y;
            return this;
        }

        public void ToScreenCoords(int x, int y, Point gridCenter)
        {
            this.X = x + gridCenter.X;
            this.Y = y - gridCenter.Y;
            // return new Point(this.OriginX + gridCenter.X, this.OriginY + gridCenter.Y);
        }

        public override void Draw(Graphics graphics, Point gridCenter)
        {
            // graphics.DrawRectangle(_pen, new Rectangle(Origin.X - 4, Origin.Y - 4, 8, 8));

            graphics.DrawRectangle(_pen, new Rectangle(
                Origin.X - 4 + gridCenter.X,
                - Origin.Y - 4 + gridCenter.Y,
                8,
                8));
        }

        public Point P
        {
            get
            {
                _origin.X = X;
                _origin.Y = Y;
                return _origin;
            }
            set
            {
                _origin = value;
            }
        }

        public override string ToString()
        {
            return $"{this.X}:{this.Y}";
        }
    }
}
