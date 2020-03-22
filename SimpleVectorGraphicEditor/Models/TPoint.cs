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

        public override Point Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public override int Width => 4;

        public int OriginX
        {
            get => _origin.X;
            set => _origin.X = value;
        }

        public int OriginY
        {
            get => _origin.Y;
            set => _origin.Y = value;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(_pen, new Rectangle(Origin.X - 4, Origin.Y - 4, 8, 8));
        }
    }
}
