using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class ExtensionBitmap
    {
        public static void DrawLineDDA(this Bitmap bmp, Color color, float x1, float y1,
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
            bmp.SetPixel((int)x, (int)y, color);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                bmp.SetPixel((int)x, (int)y, color);
            }
        }

        public static void FillRec4(this Bitmap bmp, Point pIn, Color border, Color fill)
        {
            Color color = bmp.GetPixel(pIn.X, pIn.Y);
            if ((color.R != border.R || color.G != border.G || color.B != border.B) &&
                (color.R != fill.R || color.G != fill.G || color.B != fill.B))
            {
                bmp.SetPixel(pIn.X, pIn.Y, fill);
                FillRec4(bmp, new Point(pIn.X, pIn.Y - 1), border, fill);
                FillRec4(bmp, new Point(pIn.X, pIn.Y + 1), border, fill);
                FillRec4(bmp, new Point(pIn.X + 1, pIn.Y), border, fill);
                FillRec4(bmp, new Point(pIn.X - 1, pIn.Y), border, fill);
            }
        }
        public static void FillRec8(this Bitmap bmp, Point pIn, Color border, Color fill)
        {
            Color color = bmp.GetPixel(pIn.X, pIn.Y);
            if ((color.R != border.R || color.G != border.G || color.B != border.B) &&
                (color.R != fill.R || color.G != fill.G || color.B != fill.B))
            {
                bmp.SetPixel(pIn.X, pIn.Y, fill);
                FillRec8(bmp, new Point(pIn.X, pIn.Y - 1), border, fill);
                FillRec8(bmp, new Point(pIn.X, pIn.Y + 1), border, fill);
                FillRec8(bmp, new Point(pIn.X + 1, pIn.Y), border, fill);
                FillRec8(bmp, new Point(pIn.X - 1, pIn.Y), border, fill);

                //Color ctemp1 = bmp.GetPixel(pIn.X, pIn.Y - 1);
                //Color ctemp2 = bmp.GetPixel(pIn.X - 1, pIn.Y);
                //if (!(ctemp1.R == border.R && ctemp1.G == border.G && ctemp1.B == border.B &&
                //    ctemp2.R == border.R && ctemp2.G == border.G && ctemp2.B == border.B))
                //    FillRec8(bmp, new Point(pIn.X - 1, pIn.Y - 1), border, fill);

                //ctemp1 = bmp.GetPixel(pIn.X, pIn.Y + 1);
                //ctemp2 = bmp.GetPixel(pIn.X - 1, pIn.Y);
                //if (!(ctemp1.R == border.R && ctemp1.G == border.G && ctemp1.B == border.B &&
                //    ctemp2.R == border.R && ctemp2.G == border.G && ctemp2.B == border.B))
                //FillRec8(bmp, new Point(pIn.X - 1, pIn.Y + 1), border, fill);

                //ctemp1 = bmp.GetPixel(pIn.X + 1, pIn.Y);
                //ctemp2 = bmp.GetPixel(pIn.X, pIn.Y - 1);
                //if (!(ctemp1.R == border.R && ctemp1.G == border.G && ctemp1.B == border.B &&
                //    ctemp2.R == border.R && ctemp2.G == border.G && ctemp2.B == border.B))
                //FillRec8(bmp, new Point(pIn.X + 1, pIn.Y - 1), border, fill);

                //ctemp1 = bmp.GetPixel(pIn.X + 1, pIn.Y);
                //ctemp2 = bmp.GetPixel(pIn.X, pIn.Y + 1);
                //if (!(ctemp1.R == border.R && ctemp1.G == border.G && ctemp1.B == border.B &&
                //    ctemp2.R == border.R && ctemp2.G == border.G && ctemp2.B == border.B))
                //FillRec8(bmp, new Point(pIn.X + 1, pIn.Y + 1), border, fill);
            }
        }
        public static void FillStack4(this Bitmap bmp, Point pIn, Color border, Color fill)
        {
            int[] dx = new int[] { 0, 1, 0, -1 };
            int[] dy = new int[] { -1, 0, 1, 0 };
            Stack<Point> stack = new Stack<Point>();
            stack.Push(pIn);
            while (stack.Count != 0)
            {
                Point p = stack.Pop();
                bmp.SetPixel(p.X, p.Y, fill);
                for (int i = 0; i < 4; i++)
                {
                    int nx = p.X + dx[i];
                    int ny = p.Y + dy[i];
                    Color color = bmp.GetPixel(nx, ny);
                    if ((color.R != border.R || color.G != border.G || color.B != border.B) &&
                        (color.R != fill.R || color.G != fill.G || color.B != fill.B))
                        stack.Push(new Point(nx, ny));
                }
            }
        }

        public static Bitmap Supersampling(this Bitmap bmp)
        {
            Bitmap res = new Bitmap(bmp.Width, bmp.Height);

            Color c0, c1, c2, c3;
            int r, g, b;
            List<Color> colors = new List<Color>();
            for (int y = 0; y < bmp.Height - 1; y++)
            {
                for (int x = 0; x < bmp.Width - 1; x++)
                {
                    c0 = bmp.GetPixel(x, y);
                    c1 = bmp.GetPixel(x + 1, y);
                    c2 = bmp.GetPixel(x, y + 1);
                    c3 = bmp.GetPixel(x + 1, y + 1);

                    r = (c0.R + c1.R + c2.R + c3.R) / 4;
                    g = (c0.G + c1.G + c2.G + c3.G) / 4;
                    b = (c0.B + c1.B + c2.B + c3.B) / 4;

                    res.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            for (int x = 0; x < bmp.Width; x++)
                res.SetPixel(x, bmp.Height - 1, bmp.GetPixel(x, bmp.Height - 1));
            for (int y = 0; y < bmp.Height - 1; y++)
                res.SetPixel(bmp.Width - 1, y, bmp.GetPixel(bmp.Width - 1, y));

            return res;
        }
    }
}
