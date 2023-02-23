using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifirentialEqationNumerical.Models
{
    public class Point : IPoint
    {
       public double X { get; set; }
       public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

    }
}
