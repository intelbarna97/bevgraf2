using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _GraphicsWF
{
    public partial class Form1 : Form
    {
        Graphics g;

        PointF p2;

        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            //Line segments
            g.DrawLine(Pens.Black, 30, 150, 420, 12.6f);

            Pen pRed = new Pen(Color.Red, 6.2f);
            Point p1 = new Point(50, 170);
            p2 = new PointF(440, 32.6f);
            g.DrawLine(pRed, p1, p2);

            //Rectangles
            Rectangle rectangle = new Rectangle(130, 200, 160, 70);
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 102, 0)), rectangle);
            g.DrawRectangle(Pens.Green, rectangle);
            g.FillEllipse(Brushes.Aqua, rectangle);
            g.DrawEllipse(new Pen(Color.Khaki, 3), rectangle);

            //Circle
            PointF O = new PointF(150, 130);
            float r = 50;
            g.DrawEllipse(Pens.Black, O.X - r, O.Y - r, 2 * r, 2 * r);

            //Pixel
            g.DrawRectangle(Pens.Black, 100, 100, 0.5f, 0.5f);

            //Colors
            Point p = new Point(300, 70);
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                    g.DrawRectangle(new Pen(Color.FromArgb(255, (j * j) % 256, i)), 
                        p.X + i, p.Y + j, 0.5f, 0.5f);
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
