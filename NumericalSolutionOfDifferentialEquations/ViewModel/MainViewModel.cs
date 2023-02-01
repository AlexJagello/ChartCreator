using IronPython.Hosting;
using LiveCharts;
using LiveCharts.Configurations;
using Microsoft.Scripting.Hosting;
using NumericalSolutionOfDifferentialEquations.Command;
using NumericalSolutionOfDifferentialEquations.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Point = NumericalSolutionOfDifferentialEquations.Models.Point;

namespace NumericalSolutionOfDifferentialEquations
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private InitialDataModel dataInitial;
        private ReturnDataModel dataReturn;
        private InitialDataModelDiffEquation dataInitialDiffEquation;
        private string text = "";

        private ICommand solveCommand;
        private ICommand solveDiffEquationCommand;



        public MainViewModel()
        {
            solveCommand = new RelayCommand(ClculationSimpleEquationWithPython);
            solveDiffEquationCommand = new RelayCommand(ClculationDiffEquationWithPython);
            dataInitial = new InitialDataModel();
            dataInitialDiffEquation = new InitialDataModelDiffEquation();
            dataReturn = new ReturnDataModel();         
        }

        public InitialDataModel DataInitial
        {
            get => dataInitial;
            set
            {
                if(dataInitial == value) return;   
                dataInitial = value;
                OnPropertyChanged();
            }
        }


        public InitialDataModelDiffEquation DataInitialDiffEquation
        {
            get => dataInitialDiffEquation;
            set
            {
                if (dataInitialDiffEquation == value) return;
                dataInitialDiffEquation = value;
                OnPropertyChanged();
            }
        }

        public ReturnDataModel DataReturn
        {
            get => dataReturn;
            set
            {
                if(value == dataReturn) return;
                dataReturn = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => text;
            set
            {
                if(text == value) return;
                text = value;
                OnPropertyChanged();
            }
        }

        public ICommand SolveCommand
        {
            get { return solveCommand; }
        }

        public ICommand SolveDiffEquationCommand
        {
            get { return solveDiffEquationCommand; }
        }


        public void ClculationSimpleEquationWithPython(object e)
        {
            try
            {
                AddedPointsToChart(DifirentialEqationNumerical.SimpleEquation.CalculateSimpleEquation_EnumerableReturn(dataInitial.Expression, dataInitial.X0, dataInitial.Step, dataInitial.AmountOfSteps));
            }
            catch (Exception ex)
            {

            }
        }

        public void ClculationDiffEquationWithPython(object e)
        {
            try
            {
                switch (dataInitialDiffEquation.TypeNumericalSolve)
                {
                    case TypeNumericalSolve.Eiler:
                        AddedPointsToChart(DifirentialEqationNumerical.DifferentialEquation.CalculateDifferentialEquation_EnumerableReturn_Eiler(dataInitialDiffEquation.Expression, dataInitialDiffEquation.X0, dataInitialDiffEquation.Y0, dataInitialDiffEquation.Step, dataInitialDiffEquation.AmountOfSteps));
                        break;
                    case TypeNumericalSolve.ImprovedEiler:
                        AddedPointsToChart(DifirentialEqationNumerical.DifferentialEquation.CalculateDifferentialEquation_EnumerableReturn_ImpEiler(dataInitialDiffEquation.Expression, dataInitialDiffEquation.X0, dataInitialDiffEquation.Y0, dataInitialDiffEquation.Step, dataInitialDiffEquation.AmountOfSteps));
                        break;
                    case TypeNumericalSolve.RynkeKuty:
                        AddedPointsToChart(DifirentialEqationNumerical.DifferentialEquation.CalculateDifferentialEquation_EnumerableReturn_RyngeKytte(dataInitialDiffEquation.Expression, dataInitialDiffEquation.X0, dataInitialDiffEquation.Y0, dataInitialDiffEquation.Step, dataInitialDiffEquation.AmountOfSteps));
                        break;

                }
                
            }
            catch (Exception ex)
            {

            }
        }

        private void AddedPointsToChart(IEnumerable<DifirentialEqationNumerical.Models.IPoint> points)
        {
            dataReturn.ChartValues.Clear();

            foreach (var point in points)
                dataReturn.ChartValues.Add(new Point(point.X, point.Y));

            Text = dataReturn.ToString();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
