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
        Point O;
        Point p;
        int grabbed = -1;

        public Form1()
        {
            InitializeComponent();

            O = new Point(canvas.Width / 2, canvas.Height / 2);
            p = new Point(O.X, O.Y - 100);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawCircle(Pens.Black, O,
                (int)Math.Sqrt((O.X - p.X) * (O.X - p.X) + (O.Y - p.Y) * (O.Y - p.Y)));
            g.DrawLine(Pens.Black, O, p);
            g.FillRectangle(Brushes.White, O.X - 4, O.Y - 4, 8, 8);
            g.DrawRectangle(Pens.Black, O.X - 4, O.Y - 4, 8, 8);
            g.FillRectangle(Brushes.White, p.X - 4, p.Y - 4, 8, 8);
            g.DrawRectangle(Pens.Black, p.X - 4, p.Y - 4, 8, 8);
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (O.IsGrabbedBy(e.Location))
                        grabbed = 0;
                    else if (p.IsGrabbedBy(e.Location))
                        grabbed = 1;
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
                    grabbed = -1;
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed != -1)
            {
                if (grabbed == 0) //O
                {
                    int dx = p.X - O.X;
                    int dy = p.Y - O.Y;
                    O = e.Location;
                    p.X = O.X + dx;
                    p.Y = O.Y + dy;
                }
                else if (grabbed == 1) //p
                {
                    p = e.Location;
                }
                canvas.Invalidate();
            }
        }
       
    }
}
