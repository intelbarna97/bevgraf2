using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public class BezierNCurve
    {
        public List<PointF> p = new List<PointF>();

        public BezierNCurve()
        {

        }
        public BezierNCurve(List<PointF> p)
        {
            this.p.Clear();
            for (int i = 0; i < p.Count; i++)
                this.p.Add(p[i]);
        }
    }
}
