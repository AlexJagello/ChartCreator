using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models
{
    public class ReturnDataModel : INotifyPropertyChanged
    {
        private ChartValues<IPoint> chartValues { get; set; }

        public ReturnDataModel()
        {
            var mapper = Mappers.Xy<IPoint>()
              .X(model => model.X)
              .Y(model => model.Y);

            Charting.For<IPoint>(mapper);

            chartValues = new ChartValues<IPoint>();
        }


        public ChartValues<IPoint> ChartValues 
        {
            get => chartValues;
            set 
            {
                if(chartValues.Equals(value)) return;
                chartValues = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            string str = "";

            for(int i = 0;i < chartValues.Count; i++)
            {
                str += $"{1 + i})  {chartValues[i]}\n";
            }


            return str;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
