using LiveCharts;
using NumericalSolutionOfDifferentialEquations.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.OpenAndSaveFile
{
    internal class JsonFileService : ITextFileService
    {
        public ChartValues<Point> Open(string filename)
        {
            var list = new ChartValues<Point>();

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                list = JsonSerializer.Deserialize<ChartValues<Point>>(fs);
            }

            return list;
        }

        public async void Save(string filename, ChartValues<IPoint> pointsList)
        {
            using (FileStream fs = new FileStream(filename + ".json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, pointsList);
            }
        }
    }
}
