using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class ExtensionGraphics
    {
        #region DrawPixel
        public static void DrawPixel(this Graphics g, Pen pen, float x, float y)
        {
            g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
        }
        public static void DrawPixel(this Graphics g, Color color, float x, float y)
        {
            g.DrawPixel(new Pen(color), x, y);
        }
        public static void DrawPixel(this Graphics g, Color color, PointF p)
        {
            g.DrawPixel(color, p.X, p.Y);
        }
        #endregion

        #region DrawLine
        public static void DrawLineDDA(this Graphics g, Pen pen, float x1, float y1,
                                                                 float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            float length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);
            float incX = dx / length;
            float incY = dy / length;
            float x = x1;
            float y = y1;
            g.DrawPixel(pen, x, y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                g.DrawPixel(pen, x, y);
            }
        }
        public static void DrawLineDDA(this Graphics g, Color color, float x1, float y1,
                                                                     float x2, float y2)
        {
            g.DrawLineDDA(new Pen(color), x1, y1, x2, y2);
        }
        public static void DrawLineDDA(this Graphics g, Color color1, Color color2, float x1, float y1,
                                                                                    float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            float length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);
            float incX = dx / length;
            float incY = dy / length;
            float x = x1;
            float y = y1;
            //g.DrawPixel(pen, x, y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                //g.DrawPixel(pen, x, y);
            }
        }
        public static void DrawLineMidPoint(this Graphics g, Color color, int x1, int y1,
                                                                          int x2, int y2)
        {

        }
        public static void DrawLineMidPoint(this Graphics g, Color color, float x1, float y1,
                                                                          float x2, float y2)
        {
            g.DrawLineMidPoint(color, (int)x1, (int)y1, (int)x2, (int)y1);
        }
        #endregion

        #region Cohen-Sutherland
        private const byte LEFT_CODE = 1;   //00000001
        private const byte RIGHT_CODE = 2;  //00000010
        private const byte UP_CODE = 4;     //00000100
        private const byte BOTTOM_CODE = 8;   //00001000
        private static byte OutCode(Point p, Rectangle window)
        {
            byte code = 0;  //00000000

            if (p.X < window.Left)
                code |= LEFT_CODE;
            else if (window.Right < p.X)
                code |= RIGHT_CODE;

            if (p.Y < window.Top)
                code |= UP_CODE;
            else if (window.Bottom < p.Y)
                code |= BOTTOM_CODE;

            return code;
        }

        public static void Clip(this Graphics g, Pen pen, Point p1, Point p2, Rectangle window)
        {
            byte outCode1 = OutCode(p1, window);
            byte outCode2 = OutCode(p2, window);
            bool accept = false;
            while (true)
            {
                if ((outCode1 | outCode2) == 0) //Elfogadom vágás nélkül
                {
                    accept = true;
                    break;
                }
                else if ((outCode1 & outCode2) != 0) //Elutasítom, mert kint van az egyik oldalon
                {
                    break;
                }
                else //Vágni kell
                {
                    byte code = outCode1 != 0 ? outCode1 : outCode2;
                    float x = 0, y = 0;

                    if ((code & LEFT_CODE) != 0)
                    {
                        x = window.Left;
                        y = p1.Y + (p2.Y - p1.Y) * (window.Left - p1.X) / (p2.X - p1.X);
                    }
                    else if ((code & RIGHT_CODE) != 0)
                    {
                        x = window.Right;
                        y = p1.Y + (p2.Y - p1.Y) * (window.Right - p1.X) / (p2.X - p1.X);
                    }
                    else if ((code & UP_CODE) != 0)
                    {
                        x = p1.X + (p2.X - p1.X) * (window.Top - p1.Y) / (p2.Y - p1.Y);
                        y = window.Top;
                    }
                    else if ((code & BOTTOM_CODE) != 0)
                    {
                        x = p1.X + (p2.X - p1.X) * (window.Bottom - p1.Y) / (p2.Y - p1.Y);
                        y = window.Bottom;
                    }

                    if (outCode1 != 0)
                    {
                        p1.X = (int)x; p1.Y = (int)y;
                        outCode1 = OutCode(p1, window);
                    }
                    else
                    {
                        p2.X = (int)x; p2.Y = (int)y;
                        outCode2 = OutCode(p2, window);
                    }
                }
            }
            if (accept)
                g.DrawLine(pen, p1, p2);
        }
        #endregion

        #region DrawCircle
        private static void DrawCiclePoints(this Graphics g, Pen pen, int x, int y, int xO, int yO)
        {
            g.DrawPixel(pen, +x + xO, +y + yO);
            g.DrawPixel(pen, +x + xO, -y + yO);
            g.DrawPixel(pen, -x + xO, +y + yO);
            g.DrawPixel(pen, -x + xO, -y + yO);
            g.DrawPixel(pen, +y + xO, +x + yO);
            g.DrawPixel(pen, +y + xO, -x + yO);
            g.DrawPixel(pen, -y + xO, +x + yO);
            g.DrawPixel(pen, -y + xO, -x + yO);
        }
        public static void DrawCircle(this Graphics g, Pen pen, Point O, int r)
        {
            int x = 0;
            int y = r;
            int h = 1 - r;
            g.DrawCiclePoints(pen, x, y, O.X, O.Y);
            while (y > x)
            {
                if (h < 0)
                {
                    h += 2 * x + 3;
                }
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                g.DrawCiclePoints(pen, x, y, O.X, O.Y);
            }
        }
        #endregion

        #region DrawParametricCurve
        public static void DrawParametricCurve(this Graphics g,
            Pen pen, Func<double, double> X, Func<double, double> Y,
            double a, double b, double scale = 1.0,
            double cX = 0, double cY = 0, double n = 500.0)
        {
            if (a >= b) throw new Exception("Invalid parameter interval!");

            double t = a;
            double h = (b - a) / n;
            PointF p0 = new PointF((float)(scale * X(t) + cX),
                                   (float)(scale * Y(t) + cY));
            while (t < b)
            {
                t += h;
                PointF p1 = new PointF((float)(scale * X(t) + cX),
                                       (float)(scale * Y(t) + cY));
                g.DrawLine(pen, p0, p1);
                p0 = p1;
            }
        }
        public static void DrawParametricCurve(this Graphics g,
            Color color1, Color color2, Func<double, double> X, Func<double, double> Y,
            double a, double b, double scale = 1.0,
            double cX = 0, double cY = 0, double n = 500.0)
        {
            if (a >= b) throw new Exception("Invalid parameter interval!");

            double r1, r2, g1, g2, b1, b2;

            r1 = color1.R;
            r2 = color2.R;
            g1 = color1.G;
            g2 = color2.G;
            b1 = color1.B;
            b2 = color2.B;

            double dr = r2 - r1;
            double dg = g2 - g1;
            double db = b2 - b1;

            double incR = dr / n;
            double incG = dg / n;
            double incB = db / n;

            double red = r1;
            double green = g1;
            double blue = b1;

            double t = a;
            double h = (b - a) / n;
            double half = a + (b - a) / 2;
            PointF p0 = new PointF((float)(scale * X(t) + cX),
                                   (float)(scale * Y(t) + cY));
            while (t < b)
            {
                t += h;
                PointF p1 = new PointF((float)(scale * X(t) + cX),
                                       (float)(scale * Y(t) + cY));
                Pen pen = new Pen(Color.FromArgb((int)red, (int)green, (int)blue), 4.0f);
                g.DrawLine(pen, p0, p1);
                p0 = p1;
                if(t<half)
                {
                    red += incR*2;
                    green += incG*2;
                    blue += incB*2;
                }
                else if(t<b-h)
                {
                    red -= incR * 2;
                    green -= incG * 2;
                    blue -= incB * 2;
                }
            }
        }
        #endregion

        #region DrawHermiteArc        
        public static void DrawHermiteArc(this Graphics g, Pen pen, HermiteArc arc)
        {
            g.DrawParametricCurve(pen,
                t => Hermite.H0(t) * arc.p0.X + Hermite.H1(t) * arc.p1.X + Hermite.H2(t) * arc.t0.X + Hermite.H3(t) * arc.t1.X,
                t => Hermite.H0(t) * arc.p0.Y + Hermite.H1(t) * arc.p1.Y + Hermite.H2(t) * arc.t0.Y + Hermite.H3(t) * arc.t1.Y,
                0.0, 1.0);
        }
        public static void DrawHermiteArcM(this Graphics g,
            Pen pen, HermiteArc arc)
        {
            throw new ProjectNotSolvedYetException();
        }
        public static void DrawBezier3(this Graphics g, Pen pen, Bezier3Curve curve)
        {
            g.DrawParametricCurve(pen,
                t => Bezier3.B0(t) * curve.p0.X + Bezier3.B1(t) * curve.p1.X + Bezier3.B2(t) * curve.p2.X + Bezier3.B3(t) * curve.p3.X,
                t => Bezier3.B0(t) * curve.p0.Y + Bezier3.B1(t) * curve.p1.Y + Bezier3.B2(t) * curve.p2.Y + Bezier3.B3(t) * curve.p3.Y,
                0.0, 1.0);
        }
        public static void DrawBezier3M(this Graphics g,
            Pen pen, HermiteArc arc)
        {
            throw new ProjectNotSolvedYetException();
        }
        public static void DrawBezier3K(this Graphics g, Pen pen, Bezier3KCurve curve)
        {
            g.DrawParametricCurve(pen,
                t => Bezier3K.B0(t,curve.k) * curve.p0.X + Bezier3K.B1(t, curve.k) * curve.p1.X + Bezier3K.B2(t, curve.k) * curve.p2.X + Bezier3K.B3(t, curve.k) * curve.p3.X,
                t => Bezier3K.B0(t, curve.k) * curve.p0.Y + Bezier3K.B1(t, curve.k) * curve.p1.Y + Bezier3K.B2(t, curve.k) * curve.p2.Y + Bezier3K.B3(t, curve.k) * curve.p3.Y,
                0.0, 1.0);
        }
        public static void DrawBezier3K(this Graphics g, Color color1, Color color2, Bezier3KCurve curve)
        {
            g.DrawParametricCurve(color1, color2,
                t => Bezier3K.B0(t, curve.k) * curve.p0.X + Bezier3K.B1(t, curve.k) * curve.p1.X + Bezier3K.B2(t, curve.k) * curve.p2.X + Bezier3K.B3(t, curve.k) * curve.p3.X,
                t => Bezier3K.B0(t, curve.k) * curve.p0.Y + Bezier3K.B1(t, curve.k) * curve.p1.Y + Bezier3K.B2(t, curve.k) * curve.p2.Y + Bezier3K.B3(t, curve.k) * curve.p3.Y,
                0.0, 1.0);
        }
        public static void DrawBezierN(this Graphics g, Pen pen, BezierNCurve curve)
        {
            throw new ProjectNotSolvedYetException();
        }
        public static void DrawBezierNdeCasteljouRec(this Graphics g, Pen pen, BezierNCurve curve)
        {
            throw new ProjectNotSolvedYetException();
        }
        public static void DrawBezierNdeCasteljouIter(this Graphics g, Pen pen, BezierNCurve curve)
        {
            throw new ProjectNotSolvedYetException();
        }
        public static void DrawBSpline(this Graphics g, Pen pen, BSplineCurve curve)
        {
            throw new NotImplementedException();
        }
        public static void DrawBSplineM(this Graphics g, Pen pen, BSplineCurve curve)
        {
            throw new NotImplementedException();
        }
        public static void DrawBSpline(this Graphics g, Color c0, Color c1, List<BSplineCurve> curve)
        {
            throw new ProjectNotSolvedYetException();
        }
        public static void DrawBSplineM(this Graphics g, Color c0, Color c1, List<BSplineCurve> curve)
        {
            throw new ProjectNotSolvedYetException();
        }
        #endregion
    }
}
