using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models
{
    public interface IPoint
    {
        double X { get; set; }
        double Y { get; set; }
    }
}
