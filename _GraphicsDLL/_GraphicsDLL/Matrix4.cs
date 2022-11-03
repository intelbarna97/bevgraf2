using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public class Matrix4
    {
        double[,] M = new double[4, 4];

        public Matrix4()
        {
            LoadIdentity();
        }
        public Matrix4(Matrix4 m)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    this.M[i, j] = m.M[i, j];
        }

        public void LoadIdentity()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    this.M[i, j] = 0.0;

            this.M[0, 0] = 1.0;
            this.M[1, 1] = 1.0;
            this.M[2, 2] = 1.0;
            this.M[3, 3] = 1.0;
        }
    }
}
