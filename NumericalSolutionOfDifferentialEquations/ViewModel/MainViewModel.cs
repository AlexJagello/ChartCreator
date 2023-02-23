using DifirentialEqationNumerical;
using IronPython.Hosting;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.Scripting.Hosting;
using NumericalSolutionOfDifferentialEquations.Command;
using NumericalSolutionOfDifferentialEquations.Commands;
using NumericalSolutionOfDifferentialEquations.Models;
using NumericalSolutionOfDifferentialEquations.OpenAndSaveFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Point = NumericalSolutionOfDifferentialEquations.Models.Point;

namespace NumericalSolutionOfDifferentialEquations
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private InitialDataModel dataInitial;
        private ReturnDataModel dataReturn;
        private InitialDataModelDiffEquation dataInitialDiffEquation;
        private ChartSettingsModel chartSettingsModel;
        private string text = "";

        private IDialogService dialogService;
        private ITextFileService textFileService;
        private IImgFileService imgFileService;

        private ICommand solveCommand;
        private ICommand solveDiffEquationCommand;
        private ICommand oneToOneClickCommand;
        private ICommand saveCommand;
        private ICommand loadCommand;



        internal MainViewModel(IDialogService dialogService, ITextFileService textfileService, IImgFileService imgFileService)
        {
            solveCommand = new NoParametersCommand(ClculationSimpleEquationWithPython);
            solveDiffEquationCommand = new NoParametersCommand(ClculationDiffEquationWithPython);
            oneToOneClickCommand = new NoParametersCommand(OneToOneClickMethod);
            saveCommand = new RelayCommand(SaveChartAndDataMethod);
            loadCommand = new NoParametersCommand(LoadDataMethod);

            this.dialogService = dialogService;
            this.textFileService = textfileService;
            this.imgFileService = imgFileService;

            dataInitial = new InitialDataModel();
            dataInitialDiffEquation = new InitialDataModelDiffEquation();
            dataReturn = new ReturnDataModel();         
            chartSettingsModel = new ChartSettingsModel();
        }


        #region Fields region

        public ChartSettingsModel ChartSettingsModel
        {
            get => chartSettingsModel;
            set
            {
                if (chartSettingsModel == value) return;
                    chartSettingsModel = value;
                OnPropertyChanged();
            }
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

        #endregion


        #region Command region

        public ICommand SolveCommand
        {
            get => solveCommand; 
        }

        public ICommand SolveDiffEquationCommand
        {
            get => solveDiffEquationCommand; 
        }

        public ICommand SaveCommand
        {
            get => saveCommand;
        }

        public ICommand LoadCommand
        {
            get => loadCommand;
        }

        public ICommand OneToOneClickCommand
        {
            get => oneToOneClickCommand;
        }

        public void ClculationSimpleEquationWithPython()
        {
            try
            {
                AddedPointsToChart(
                    SimpleEquation.CalculateSimpleEquation_EnumerableReturn(
                        dataInitial.Expression,
                        dataInitial.X0,
                        dataInitial.Step,
                        dataInitial.AmountOfSteps,
                        PythonCalculationClass.PythonCalculation
                        ));
            }
            catch (Exception ex)
            {

            }
        }

        public void ClculationDiffEquationWithPython()
        {
            try
            {
                switch (dataInitialDiffEquation.TypeNumericalSolve)
                {
                    case TypeNumericalSolve.Eiler:
                        AddedPointsToChart(
                            DifferentialEquation.CalculateDifferentialEquation_EnumerableReturn_Eiler(
                                dataInitialDiffEquation.Expression, 
                                dataInitialDiffEquation.X0,
                                dataInitialDiffEquation.Y0, 
                                dataInitialDiffEquation.Step, 
                                dataInitialDiffEquation.AmountOfSteps,
                                PythonCalculationClass.PythonCalculation_DifferentialEquation
                                ));
                        break;
                    case TypeNumericalSolve.ImprovedEiler:
                        AddedPointsToChart(
                            DifferentialEquation.CalculateDifferentialEquation_EnumerableReturn_ImpEiler(
                                dataInitialDiffEquation.Expression, 
                                dataInitialDiffEquation.X0, 
                                dataInitialDiffEquation.Y0, 
                                dataInitialDiffEquation.Step, 
                                dataInitialDiffEquation.AmountOfSteps,
                                PythonCalculationClass.PythonCalculation_DifferentialEquation
                                ));
                        break;
                    case TypeNumericalSolve.RynkeKuty:
                        AddedPointsToChart(
                            DifferentialEquation.CalculateDifferentialEquation_EnumerableReturn_RyngeKytte(
                                dataInitialDiffEquation.Expression, 
                                dataInitialDiffEquation.X0, 
                                dataInitialDiffEquation.Y0, 
                                dataInitialDiffEquation.Step, 
                                dataInitialDiffEquation.AmountOfSteps,
                                PythonCalculationClass.PythonCalculation_DifferentialEquation
                                ));
                        break;

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void OneToOneClickMethod()
        {
            try
            {
                ChartSettingsModel.AxisMinX = dataReturn.ChartValues.First().X;
                ChartSettingsModel.AxisMaxX = dataReturn.ChartValues.Last().X;
                ChartSettingsModel.AxisMinY = dataReturn.ChartValues.Min(min => min.Y);
                ChartSettingsModel.AxisMaxY = dataReturn.ChartValues.Max(max => max.Y);
            }
            catch
            {

            }

        }


        #region SaveRegion
        public void SaveChartAndDataMethod(object obj)
        {
            try
            {
                if (dialogService.SaveFileDialog() == true)
                {
                    var path = dialogService.FilePath;
                    
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    textFileService.Save(path + "/data", dataReturn.ChartValues);
                    imgFileService.Save(path + "/image", dataReturn.ChartValues);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        #endregion

        public void LoadDataMethod()
        {
            try
            {
                if (dialogService.OpenFileDialog() == true)
                {
                    AddedPointsToChart(textFileService.Open(dialogService.FilePath));    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddedPointsToChart(IEnumerable<IPoint> points)
        {
            dataReturn.ChartValues.Clear();

            foreach (var point in points)
                dataReturn.ChartValues.Add(new Point(point.X, point.Y));

            OneToOneClickMethod();


            Text = dataReturn.ToString();
        }

        private void AddedPointsToChart(IEnumerable<DifirentialEqationNumerical.Models.IPoint> points)
        {
            dataReturn.ChartValues.Clear();

            foreach (var point in points)
                dataReturn.ChartValues.Add(new Point(point.X, point.Y));

            OneToOneClickMethod();


            Text = dataInitial.Expression + "\n"
              + $"x0={dataInitial.X0}, h={dataInitial.Step}, n={dataInitial.AmountOfSteps}\n\n"
              + dataReturn.ToString();
        }

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
