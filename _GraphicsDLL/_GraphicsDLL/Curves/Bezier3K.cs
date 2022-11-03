using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class Bezier3K
    {
        public static double B0(double t, double k) { return (1 - t) * (1 - t) * (1 + (2 - k) * t); }
        public static double B1(double t, double k) { return k * t * (1 - t) * (1 - t); }
        public static double B2(double t, double k) { return k * t * t * (1 - t); }
        public static double B3(double t, double k) { return t * t * (3 - k + (k - 2) * t); }

        public static Matrix4 MatrixB(double t, double k) { throw new NotImplementedException(); }
    }
}
