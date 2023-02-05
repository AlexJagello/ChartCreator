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
    public class MainViewModel : INotifyPropertyChanged
    {
        private InitialDataModel dataInitial;
        private ReturnDataModel dataReturn;
        private InitialDataModelDiffEquation dataInitialDiffEquation;
        private ChartSettingsModel chartSettingsModel;
        private string text = "";

        private ICommand solveCommand;
        private ICommand solveDiffEquationCommand;
        private ICommand oneToOneClickCommand;
        private ICommand saveCommand;
        private ICommand loadCommand;



        public MainViewModel()
        {
            solveCommand = new NoParametersCommand(ClculationSimpleEquationWithPython);
            solveDiffEquationCommand = new NoParametersCommand(ClculationDiffEquationWithPython);
            oneToOneClickCommand = new NoParametersCommand(OneToOneClickMethod);
            saveCommand = new RelayCommand(SaveChartAndDataMethod);
            loadCommand = new NoParametersCommand(LoadDataMethod);


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
                OneToOneClickMethod();
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
                OneToOneClickMethod();
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
                ChartSettingsModel.AxisMinY = dataReturn.ChartValues.First().Y;
                ChartSettingsModel.AxisMaxX = dataReturn.ChartValues.Last().X;
                ChartSettingsModel.AxisMaxY = dataReturn.ChartValues.Last().Y;
            }
            catch
            {

            }

        }


        #region SaveRegion
        public void SaveChartAndDataMethod(object obj)
        {
            string path = "Chart_Image_" + DateTime.Now.ToShortDateString();

            SaveChart(chartSettingsModel.AxisMaxX, chartSettingsModel.AxisMinX, chartSettingsModel.AxisMaxY, chartSettingsModel.AxisMinY, path);

            SaveData(path);
        }

        private async void SaveData(string pathToFile)
        {
            using (FileStream fs = new FileStream(pathToFile + ".json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, dataReturn.ChartValues);
            }
        }

        private void SaveChart(double MaxValueX, double MinValueX, double MaxValueY, double MinValueY, string pathToFile)
        {
            var chartToSave = new LiveCharts.Wpf.CartesianChart
            {
                DisableAnimations = true,
                Width = 600,
                Height = 400,
                AxisX = { new Axis() { MaxValue = MaxValueX, MinValue = MinValueX } },
                AxisY = { new Axis() { MaxValue = MaxValueY, MinValue = MinValueY } },
                Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Values = dataReturn.ChartValues
                    }

                }
            };


            var viewbox = new Viewbox();
            viewbox.Child = chartToSave;
            viewbox.Measure(chartToSave.RenderSize);
            viewbox.Arrange(new Rect(new System.Windows.Point(0, 0), chartToSave.RenderSize));
            chartToSave.Update(true, true);
            viewbox.UpdateLayout();

            SaveToPng(chartToSave, pathToFile + ".png");
        }

        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }

        #endregion

        public void LoadDataMethod()
        {
            //TO DO 
        }



        private void AddedPointsToChart(IEnumerable<DifirentialEqationNumerical.Models.IPoint> points)
        {
            dataReturn.ChartValues.Clear();

            foreach (var point in points)
                dataReturn.ChartValues.Add(new Point(point.X, point.Y));

            Text = dataReturn.ToString();
        }

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
