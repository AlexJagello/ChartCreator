using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models.Interfaces
{
    public interface IDataInitial
    {
        double X0 { get; set; }

        double Step { get; set; }

        int AmountOfSteps { get; set; }

        string Expression { get; set; }


    }
}
