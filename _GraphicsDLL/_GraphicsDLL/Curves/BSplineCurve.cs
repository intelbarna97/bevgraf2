using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public class BSplineCurve
    {
        public PointF p0, p1, p2, p3;

        public BSplineCurve(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
    }
}
