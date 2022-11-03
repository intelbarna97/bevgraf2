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
        Random rnd = new Random();
        PointF topLeft = new PointF(100, 100);
        float size = 200;
        Brush brush = Brushes.Red;
        float velocityX = 4, velocityY = 3;
        float dx = 0, dy = 0;
        bool grabbed = false;

        public Form1()
        {
            InitializeComponent();
            timer.Start();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.FillRectangle(brush, topLeft.X, topLeft.Y, size, size);
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (topLeft.X <= e.X && e.X < topLeft.X + size &&
                        topLeft.Y <= e.Y && e.Y < topLeft.Y + size)
                    {
                        dx = e.X - topLeft.X;
                        dy = e.Y - topLeft.Y;
                        grabbed = true;
                    }
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
                    if (grabbed)
                    {
                        brush = new SolidBrush(Color.FromArgb(rnd.Next(256), 
                                                              rnd.Next(256), 
                                                              rnd.Next(256)));
                        size -= 30;
                        velocityX++;
                        velocityY++;
                        velocityX *= rnd.NextDouble() < 0.5 ? 1 : -1;
                        velocityY *= rnd.NextDouble() < 0.5 ? 1 : -1;
                        grabbed = false;
                    }
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed)
            {
                topLeft = new PointF(e.X - dx, e.Y - dy);

                if (topLeft.X < 0)
                    topLeft.X = 0;
                canvas.Invalidate();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!grabbed)
            {
                topLeft.X += velocityX;
                topLeft.Y += velocityY;
                if (topLeft.X < 0 || topLeft.X > canvas.Width - size)
                    velocityX *= -1;
                canvas.Invalidate();
            }
        }

    }
}
