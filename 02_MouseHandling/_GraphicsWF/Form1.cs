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
        PointF topLeft = new PointF(100, 100);
        float size = 200;
        float dx = 0, dy = 0;
        Brush brush = Brushes.Red;
        bool grabbed = false;        

        public Form1()
        {
            InitializeComponent();            
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
                    grabbed = false;
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
       
    }
}
