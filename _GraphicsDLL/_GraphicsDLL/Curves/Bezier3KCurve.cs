using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public class Bezier3KCurve
    {
        public PointF p0, p1, p2, p3;
        public double k;

        public Bezier3KCurve(PointF p0, PointF p1, PointF p2, PointF p3)
            : this(p0, p1, p2, p3, 1.0)
        {
        }
        public Bezier3KCurve(PointF p0, PointF p1, PointF p2, PointF p3, double k)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.k = k;
        }
    }
}
