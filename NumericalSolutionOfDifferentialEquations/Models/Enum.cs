using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models
{
    public enum Type
    {
        [Description("Simple equation")]
        Simple,
        [Description("First order differential equation")]
        FirstOrderDifferentialEquation
    }
}
