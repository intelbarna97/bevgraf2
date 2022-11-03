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
        List<PointF> p = new List<PointF>();
        int grabbed = -1;
        Color color = Color.Black;

        public Form1()
        {
            InitializeComponent();            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            for (int i = 0; i < p.Count - 1; i++)            
                g.DrawLineDDA(Pens.Black, p[i].X, p[i].Y, p[i + 1].X, p[i + 1].Y);
            for (int i = 0; i < p.Count; i++)
            {
                g.FillRectangle(new SolidBrush(canvas.BackColor), p[i].X - ExtensionPointF.GRAB_DISTANCE,
                                                                  p[i].Y - ExtensionPointF.GRAB_DISTANCE,
                                                                  2 * ExtensionPointF.GRAB_DISTANCE,
                                                                  2 * ExtensionPointF.GRAB_DISTANCE);
                g.DrawRectangle(Pens.Black, p[i].X - ExtensionPointF.GRAB_DISTANCE,
                                            p[i].Y - ExtensionPointF.GRAB_DISTANCE,
                                            2 * ExtensionPointF.GRAB_DISTANCE,
                                            2 * ExtensionPointF.GRAB_DISTANCE);
            }
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    for (int i = 0; i < p.Count; i++)
                    {
                        if (p[i].IsGrabbedBy(e.Location))
                            grabbed = i;
                    }
                    if (grabbed == -1)
                    {
                        p.Add(new PointF(e.X, e.Y));
                        grabbed = p.Count - 1;
                        canvas.Invalidate();
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
                p[grabbed] = new PointF(e.X, e.Y);
                canvas.Invalidate();
            }
        }
       
    }
}
