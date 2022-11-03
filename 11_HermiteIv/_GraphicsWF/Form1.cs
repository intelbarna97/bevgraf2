using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _GraphicsDLL;

namespace _GraphicsWF
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF p0 = new PointF(100, 400), 
               p1 = new PointF(500, 100), 
               t0 = new PointF(150, 80), 
               t1 = new PointF(600, 380);
        int grabbed = -1;

        Pen pControl = Pens.Black;
        Pen pCurve = new Pen(Color.Blue, 2f);
        Brush bBackground;

        public Form1()
        {
            InitializeComponent();
            bBackground = new SolidBrush(canvas.BackColor);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.DrawLine(pControl, p0, t0);
            g.DrawLine(pControl, p1, t1);
            g.FillRectangle(bBackground, p0.X - 4, p0.Y - 4, 8, 8);
            g.DrawRectangle(pControl, p0.X - 4, p0.Y - 4, 8, 8);
            g.FillRectangle(bBackground, p1.X - 4, p1.Y - 4, 8, 8);
            g.DrawRectangle(pControl, p1.X - 4, p1.Y - 4, 8, 8);

            float lambda = 2.5f;
            HermiteArc arc = new HermiteArc(p0, p1, 
                new PointF(lambda * (t0.X - p0.X), lambda * (t0.Y - p0.Y)),
                new PointF(lambda * (t1.X - p1.X), lambda * (t1.Y - p1.Y)));
            g.DrawHermiteArc(pCurve, arc);
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
       
    }
}
