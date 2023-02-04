using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models
{
    public class Point : IPoint
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {Math.Round(X,3)}, Y: {Math.Round(Y,3)};";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if(obj.GetType() != this.GetType()) return false;
           
            Point p = (Point)obj;
            if(p.X != X || p.Y != Y) return false;
           
            return true;
        }

        public override int GetHashCode()
        {
            return (int)X ^ (int)Y;
        }
    }
}
