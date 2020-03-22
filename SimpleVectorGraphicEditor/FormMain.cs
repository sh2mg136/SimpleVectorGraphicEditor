using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleVectorGraphicEditor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public enum Tools { Line, Circle, Rectangle }

        #region Declarations

        /// <summary>
        /// Размер ячейки секи
        /// </summary>
        private const int GRID_SIZE = 20;

        private List<TShape> _shapes = new List<TShape>();
        private TShape _shape = null;
        private TPoint _pointer = new TPoint();
        private bool _captured = false;
        private Size grid = new Size(GRID_SIZE, GRID_SIZE);
        private Size halfGrid = new Size(0, 0);
        private Tools _selectedTool = Tools.Circle;

        #endregion Declarations

        private void Form1_Load(object sender, EventArgs e)
        {
            var rect = Screen.GetBounds(this);
            this.Width = (int)(rect.Width * 0.75);
            this.Height = (int)(rect.Height * 0.75);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.splitContainer2.Dock = DockStyle.Fill;

            toolLine.Tag = Tools.Line;
            toolCircle.Tag = Tools.Circle;
            toolRectangle.Tag = Tools.Rectangle;

            toolLine.Click += ToolButton_Click;
            toolCircle.Click += ToolButton_Click;
            toolRectangle.Click += ToolButton_Click;

            Color formColor = this.BackColor;

            SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.Opaque
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.UserPaint,
                true);

            this.BackColor = formColor;

            TCircle circle = new TCircle(new TPoint(60, 60), 80);
            _shapes.Add(circle);

            TLine line = new TLine(new TPoint(-20, -20), new TPoint(40, 40));
            _shapes.Add(line);

            TGrid.SetCenter(box.Width / 2, box.Height / 2);
        }

        private void ToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedTool = (Tools)(sender as ToolStripButton).Tag;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                _selectedTool = Tools.Line;
            }
        }

        private void BOX_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Wheat);

            TGrid.Draw(e.Graphics, box.Width, box.Height, GRID_SIZE);

            foreach (TShape shape in _shapes)
            {
                shape.Draw(e.Graphics, TGrid.Origin);
            }

            if (_shape != null)
            {
                _shape.Draw(e.Graphics, TGrid.Origin);
            }

            _pointer.Draw(e.Graphics, TGrid.Origin);

            base.OnPaint(e);
        }

        private void BOX_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _captured = true;

                var p = UpdateCursor(e.Location);

                SetText(p.ToString(), false);

                switch (_selectedTool)
                {
                    case Tools.Line:
                        {
                            _shape = new TLine(p, p);
                        }
                        break;

                    case Tools.Circle:
                        {
                            _shape = new TCircle(p, 0);
                        }
                        break;

                    case Tools.Rectangle:
                        {
                            _shape = new TRectangle(p, p);
                        }
                        break;

                    default:
                        break;
                }
            }

            base.OnMouseDown(e);
        }

        private void BOX_MouseMove(object sender, MouseEventArgs e)
        {
            var p = UpdateCursor(e.Location);

            if (_captured)
            {
                UpdateShapeUnderConstruction(p);
            }

            base.OnMouseMove(e);
        }

        private void BOX_MouseUp(object sender, MouseEventArgs e)
        {
            if (_captured && e.Button == MouseButtons.Left)
            {
                _captured = false;
                var p = UpdateCursor(e.Location);
                Task.Run(() => SetText(p.ToString(), false));
                UpdateShapeUnderConstruction(p);
                if (_shape.Width > 0)
                {
                    _shapes.Add(_shape);
                }
                _shape = null;
            }

            UpdateCursor(e.Location);
            base.OnMouseUp(e);
        }

        private TPoint UpdateCursor(Point e)
        {
            halfGrid = new Size(grid.Width / 2, grid.Height / 2);

            /*
            var snapX = (((e.X + halfGrid.Width) / grid.Width) * grid.Width);
            var snapY = (((e.Y + halfGrid.Height) / grid.Height) * grid.Height);
            _pointer.OriginX = snapX;
            _pointer.OriginY = snapY;
            */

            _pointer.ToUserCoords(e, TGrid.Origin);
            //var snapX = (((_pointer.OriginX + halfGrid.Width) / grid.Width) * grid.Width);
            //var snapY = (((_pointer.OriginY + halfGrid.Height) / grid.Height) * grid.Height);

            var snapX = (((_pointer.X - halfGrid.Width) / grid.Width) * grid.Width);
            var snapY = (((_pointer.Y - halfGrid.Height) / grid.Height) * grid.Height);


            //_pointer.ToScreenCoords(snapX, snapY, TGrid.Origin);
            //_pointer.OriginX = snapX + TGrid.Origin.X;
            //_pointer.OriginY = snapY + TGrid.Origin.Y;
            _pointer.X = snapX;
            _pointer.Y = snapY;

            box.Invalidate();

            /*
                        Task.Run(() => SetText($@"{_pointer}
            {TGrid.Origin.X}:{TGrid.Origin.Y}
            {_pointer.Origin.X - TGrid.Origin.X}:{_pointer.Origin.Y - TGrid.Origin.Y}", true));
            */

            return _pointer; // .ToUserCoords(TGrid.Origin).Origin;
        }

        private void SetText(string msg, bool clear)
        {
            if (InvokeRequired)
            {
                Invoke((Action<string, bool>)SetText, msg, clear);
                return;
            }
            if (clear) richTextBox1.Clear();
            richTextBox1.AppendText(msg);
            richTextBox1.AppendText(Environment.NewLine);
        }

        private void UpdateShapeUnderConstruction(TPoint point)
        {
            if (_shape == null)
                return;

            TPoint origin = _shape.Origin;

            switch (_selectedTool)
            {
                case Tools.Line:
                    (_shape as TLine).Finish = point;
                    break;

                case Tools.Circle:
                    {
                        int xRadius = Math.Abs(origin.X - point.X);
                        int yRadius = Math.Abs(origin.Y - point.Y);
                        (_shape as TCircle).Radius = Math.Min(xRadius, yRadius);
                    }
                    break;

                case Tools.Rectangle:
                    (_shape as TRectangle).Finish = point;
                    break;

                default:
                    break;
            }

            // if ((ModifierKeys & Keys.Shift) == 0) xRadius = yRadius = Math.Max(xRadius, yRadius);

            box.Invalidate();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _selectedTool = Tools.Circle;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _selectedTool = Tools.Rectangle;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            TGrid.SetCenter(box.Width / 2, box.Height / 2);
            box.Invalidate();
        }
    }
}