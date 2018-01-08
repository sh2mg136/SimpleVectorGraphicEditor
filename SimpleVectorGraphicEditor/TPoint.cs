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

        public override Point Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public override int Width => 4;

        Pen pen = Pens.Lime;

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(pen, new Rectangle(Origin.X - 4, Origin.Y - 4, 8, 8));
        }
    }
}
