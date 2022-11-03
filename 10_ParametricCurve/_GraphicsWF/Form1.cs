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

        public Form1()
        {
            InitializeComponent();            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            double R = 50;
            PointF O = new PointF(canvas.Width / 2, canvas.Height / 2);
            g.DrawParametricCurve(new Pen(Color.DarkCyan, 3f),
                t => R * Math.Cos(t) + O.X,
                t => R * Math.Sin(t) + O.Y,
                0, 2 * Math.PI);

            //g.DrawParametricCurve(new Pen(Color.DarkCyan, 3f),
            //    t => t,
            //    t => Math.Sin(t),
            //    -2 * Math.PI, 2 * Math.PI,
            //    75, 
            //    canvas.Width / 2, canvas.Height / 2, 500);

            //double a = -2 * Math.PI;
            //double b = 2 * Math.PI;
            //double t = a;
            //double h = (b - a) / 500.0;
            //float scale = 50;
            //PointF O = new PointF(canvas.Width / 2, canvas.Height / 2);
            //PointF p0 = new PointF(scale * (float)t + O.X, 
            //                       scale * (float)Math.Sin(t) + O.Y);
            //while (t < b)
            //{
            //    t += h;
            //    PointF p1 = new PointF(scale * (float)t + O.X,
            //                           scale * (float)Math.Sin(t) + O.Y);
            //    g.DrawLine(Pens.Black, p0, p1);
            //    p0 = p1;
            //}
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
