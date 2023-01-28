using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models.Interfaces
{
    public interface IDataReturn
    {
        ChartValues<IPoint> ChartValues { get; set; }
    }
}
