using LiveCharts;
using NumericalSolutionOfDifferentialEquations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.OpenAndSaveFile
{
    internal interface IImgFileService
    {
        void Save(string filename, ChartValues<IPoint> pointsList);
    }
}
