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
        Rectangle window;
        Pen penWindow = new Pen(Color.Black, 3f);
        Pen penFullLine = Pens.Gray;
        List<Point> p = new List<Point>();
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            window = new Rectangle(50, 70, canvas.Width - 160, canvas.Height - 120);
            for (int i = 0; i < 100; i++)
            {
                p.Add(new Point(rnd.Next(canvas.Width), rnd.Next(canvas.Height)));
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            for (int i = 0; i < p.Count - 1; i += 2)
            {
                g.DrawLine(penFullLine, p[i], p[i + 1]);
                g.Clip(new Pen(Color.Green, 2f), p[i], p[i + 1], window);
            }
            g.DrawRectangle(penWindow, window);
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
