using NumericalSolutionOfDifferentialEquations.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NumericalSolutionOfDifferentialEquations.OpenAndSaveFile
{
    internal class PngFileService : IImgFileService
    {
        public void Save(string filename, ChartValues<IPoint> pointsList)
        {
            var chartToSave = new CartesianChart
            {
                DisableAnimations = true,
                Width = 600,
                Height = 400,
                AxisX = { new Axis() { MaxValue = pointsList[pointsList.Count - 1].X, MinValue = pointsList[0].X } },
                AxisY = { new Axis() { MaxValue = pointsList[pointsList.Count - 1].Y, MinValue = pointsList[0].Y } },
                Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Values = pointsList
                    }

                }
            };


            var viewbox = new Viewbox();
            viewbox.Child = chartToSave;
            viewbox.Measure(chartToSave.RenderSize);
            viewbox.Arrange(new Rect(new System.Windows.Point(0, 0), chartToSave.RenderSize));
            chartToSave.Update(true, true);
            viewbox.UpdateLayout();

            SaveToPng(chartToSave, filename + ".png");
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
    }
}
