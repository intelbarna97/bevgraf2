using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    class ProjectNotSolvedYetException : Exception
    {
        public ProjectNotSolvedYetException()
        {

        }
        public ProjectNotSolvedYetException(string message)
            : base(message)
        {

        }
    }
}
