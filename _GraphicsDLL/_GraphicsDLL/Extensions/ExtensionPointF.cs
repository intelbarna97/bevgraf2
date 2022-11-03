using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class ExtensionPointF
    {
        public static int GRAB_DISTANCE = 5;

        public static bool IsGrabbedBy(this PointF p, PointF mouse)
        {
            return Math.Abs(p.X - mouse.X) < GRAB_DISTANCE &&
                   Math.Abs(p.Y - mouse.Y) < GRAB_DISTANCE;
        }
        public static bool IsGrabbedBy(this Point p, Point mouse)
        {
            return Math.Abs(p.X - mouse.X) < GRAB_DISTANCE &&
                   Math.Abs(p.Y - mouse.Y) < GRAB_DISTANCE;
        }
        public static bool IsGrabbedBy2(this PointF p, PointF mouse)
        {
            return Math.Sqrt((p.X - mouse.X) * (p.X - mouse.X) +
                             (p.Y - mouse.Y) * (p.Y - mouse.Y)) < GRAB_DISTANCE;
        }
    }
}
