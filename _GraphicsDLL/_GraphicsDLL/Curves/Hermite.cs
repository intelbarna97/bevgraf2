using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class Hermite
    {
        public static double H0(double t) { return 2 * t * t * t - 3 * t * t + 1; }
        public static double H1(double t) { return -2 * t * t * t + 3 * t * t; }
        public static double H2(double t) { return t * t * t - 2 * t * t + t; }
        public static double H3(double t) { return t * t * t - t * t; }

        public static Matrix4 MatrixH()
        {
            throw new NotImplementedException();
        }
    }
}
