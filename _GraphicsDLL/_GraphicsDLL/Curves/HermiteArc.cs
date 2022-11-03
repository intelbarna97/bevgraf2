using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public class HermiteArc
    {
        public HermiteArc(PointF p0, PointF p1, PointF t0, PointF t1, double lambda = 1.0)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.t0 = t0;
            this.t1 = t1;
            this.lambda = lambda;
        }

        public double lambda;
        public PointF p0, p1, t0, t1;
    }
}
