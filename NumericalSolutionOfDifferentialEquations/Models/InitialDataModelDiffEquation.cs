using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models
{
    public class InitialDataModelDiffEquation : InitialDataModel
    {
        private double y0 = 1;
        private TypeNumericalSolve typeNumericalSolve = TypeNumericalSolve.Eiler;


        public InitialDataModelDiffEquation()
        {
            Expression = "dy=pow(x,2)-2*y";
        }

        public double Y0
        {
            get => y0;
            set
            {
                if (y0 == value) return;
                y0 = value;
                OnPropertyChanged();
            }
        }

        public TypeNumericalSolve TypeNumericalSolve
        {
            get => typeNumericalSolve;
            set
            {
                if(typeNumericalSolve == value) return; 
                typeNumericalSolve = value;
                OnPropertyChanged();
            }
        }
    }
}
