using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    struct Vector2
    {
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        double x, y;

        public static Vector2 operator +(Vector2 v0, Vector2 v1)
        {
            return new Vector2(v0.x + v1.x, v0.y + v1.y);
        }
    }
}
