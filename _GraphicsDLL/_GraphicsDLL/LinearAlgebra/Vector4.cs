using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public class Vector4
    {
        double x, y, z, w;

        public Vector4() : this(0.0, 0.0, 0.0, 0.0) { }
        public Vector4(Vector4 v) : this(v.x, v.y, v.z, v.w) { }
        public Vector4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }
}
