using IronPython.Hosting;
using LiveCharts;
using LiveCharts.Configurations;
using Microsoft.Scripting.Hosting;
using NumericalSolutionOfDifferentialEquations.Command;
using NumericalSolutionOfDifferentialEquations.Models;
using NumericalSolutionOfDifferentialEquations.Models.Interfaces;
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
        private IDataInitial dataInitial;
        private IDataReturn dataReturn;
        private string text = "";

        private ICommand solveCommand;
        private Models.Type type;



        public MainViewModel()
        {
            solveCommand = new RelayCommand(ClculationWithPython);
            dataInitial = new InitialDataModel();
            dataReturn = new ReturnDataModel();         
        }



        public Models.Type Type
        {
            get => type;
            set
            {
                if (type == value) return;
                type = value;
                OnPropertyChanged();
            }
        }

        public IDataInitial DataInitial
        {
            get => dataInitial;
            set
            {
                if(dataInitial == value) return;   
                dataInitial = value;
                OnPropertyChanged();
            }
        }

        public IDataReturn DataReturn
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




        public void ClculationWithPython(object e)
        {
           var points = DifirentialEqationNumerical.SimpleEquation.CalculateSimpleEquation_EnumerableReturn(dataInitial.Expression, dataInitial.X0, dataInitial.Step, dataInitial.AmountOfSteps);

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
