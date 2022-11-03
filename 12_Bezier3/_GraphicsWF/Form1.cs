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

        Pen penControl = new Pen(Color.Black, 2f);
        Color color1, color2;

        PointF P1, P2, newPoint;


        float m, yintersect, k=3;


        List<PointF> P = new List<PointF>();

        int found = -1;
        int radius = 5;

        public Form1()
        {
            InitializeComponent();
            trackBar1.Value = (int)k;
            label1.Text = k.ToString();
            List<Color> colors = new List<Color>();
            List<Color> colors1 = new List<Color>();
            colors.Add(Color.Black);
            colors.Add(Color.Blue);
            colors.Add(Color.Red);
            colors.Add(Color.Green);
            colors.Add(Color.Yellow);
            colors.Add(Color.Orange);
            colors.Add(Color.Purple);
            colors1.AddRange(colors);
            comboBox1.DataSource = colors;
            comboBox2.DataSource = colors1;
            color1 = (Color)comboBox1.SelectedItem;
            color2 = (Color)comboBox2.SelectedItem;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (P.Count >= 4)
            {
                for (int i = 0; i < P.Count - 3; i = i + 3)
                {
                    g.DrawBezier3K(color1, color2, new Bezier3KCurve(P[i], P[i + 1], P[i + 2], P[i + 3], k));
                }
                if ((P.Count - 1) % 3 == 0)
                {
                    P1 = P[P.Count - 1];
                    P2 = P[P.Count - 2];
                    if (P1.X < P2.X)
                        newPoint.X = 0;
                    else
                        newPoint.X = canvas.Width;
                    newPoint.Y = GetY(newPoint.X);
                    g.DrawLine(Pens.Black, P2, newPoint);
                }

            }

            for (int i = 0; i < P.Count - 1; i++)
            {
                g.DrawLineDDA(penControl, P[i].X, P[i].Y, P[i + 1].X, P[i + 1].Y);
            }

            for (int i = 0; i < P.Count; i++)
            {
                g.FillEllipse(Brushes.White, P[i].X - radius, P[i].Y - radius, 2 * radius, 2 * radius);
                g.DrawEllipse(Pens.Black, P[i].X - radius, P[i].Y - radius, 2 * radius, 2 * radius);
            }
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:

                    for (int i = 0; i < P.Count; i++)
                    {
                        if (Math.Abs(e.X - P[i].X) <= radius && Math.Abs(e.Y - P[i].Y) <= radius)
                            found = i;
                    }

                    if (found == -1)
                    {
                        if (P.Count > 1 && (P.Count - 1) % 3 == 0)
                        {
                            PointF newPoint = GetPointOnLine(e.Location);
                            if(P1.Y>P2.Y && newPoint.Y>P1.Y)
                            {
                                P.Add(newPoint);
                            }

                            if (P1.Y < P2.Y && newPoint.Y<P1.Y)
                            {
                                P.Add(newPoint);
                            }

                            if(P1.Y == P2.Y)
                            {
                                if(P1.X > P2.X && newPoint.X>P1.X)
                                {
                                    P.Add(newPoint);
                                }
                                if (P1.X < P2.X && newPoint.X < P1.X)
                                {
                                    P.Add(newPoint);
                                }
                            }
                        }
                        else
                        {
                            P.Add(e.Location);
                            found = P.Count - 1;
                        }

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
                    found = -1;
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (found != -1)
            {
                if (found != 0 && found != 1 && (found + 2) % 3 == 0) // 3.pont
                {
                    P[found] = GetNewPoints(P[found], P[found - 1], e.Location);
                }
                else if (found != 0 && found != 1 && found % 3 == 0 && found + 1 < P.Count) // 2.pont
                {
                    P[found] = e.Location;
                    P[found + 1] = GetNewPoints(P[found - 1], e.Location, P[found + 1]); //a félegyenesen lévő pont új helye
                }
                else if (found != 0 && found != 1 && (found + 1) % 3 == 0 && found + 2 < P.Count) // 1.pont
                {
                    P[found] = e.Location;
                    P[found + 2] = GetNewPoints(e.Location, P[found + 1], P[found + 2]);
                }
                else
                {
                    P[found] = e.Location;
                }
                canvas.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            P.Clear();
            k = 3;
            trackBar1.Value = (int)k;
            label1.Text = k.ToString();
            canvas.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem != null && comboBox2.SelectedItem!=null)
            {
                color1 = (Color)comboBox1.SelectedItem;
                color2 = (Color)comboBox2.SelectedItem;
            }
            canvas.Invalidate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                color1 = (Color)comboBox1.SelectedItem;
                color2 = (Color)comboBox2.SelectedItem;
            }
            canvas.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            k = trackBar1.Value;
            label1.Text = k.ToString();
            canvas.Invalidate();
        }

        private float GetY(float X)
        {
            m = (P2.Y - P1.Y) / (P2.X - P1.X); //meredekség
            yintersect = -m * P1.X + P1.Y;
            return m * X + yintersect;
        }

        private PointF GetNewPoints(PointF p1, PointF p2, PointF p3)
        {
            PointF newP = new PointF();
            m = (p2.Y - p1.Y) / (p2.X - p1.X);
            newP.X = p3.X;
            newP.Y = m * (newP.X - p1.X) + p1.Y;
            return newP;
        }

        private PointF GetPointOnLine(PointF point)
        {
            PointF newPoint = new PointF();
            float newSlope, b;

            m = (P2.Y - P1.Y) / (P2.X - P1.X);
            newSlope = (1 / m)*(-1);
            b = -newSlope * point.X + point.Y;
            
            newPoint.X = (yintersect - b) / (newSlope - m);
            newPoint.Y = m * newPoint.X + yintersect;
            return newPoint;
        }

    }
}
