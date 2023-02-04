using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NumericalSolutionOfDifferentialEquations.Models
{
    public class ChartSettingsModel : INotifyPropertyChanged
    {
        private double axisMaxX = 10;
        private double axisMinX = 0;
        private double axisMaxY = 10;
        private double axisMinY = 0;
        private double axisUnitX;
        private double axisUnitY;
        private double axisStepX;
        private double axisStepY;

        public double AxisMaxX
        {
            get => axisMaxX;
            set
            {               
                axisMaxX = value;
                OnPropertyChanged();
            }
        }

        public double AxisMinX
        {
            get => axisMinX;
            set
            {
                axisMinX = value;
                OnPropertyChanged();
            }
        }

        public double AxisMaxY
        {
            get => axisMaxY;
            set
            {
                axisMaxY = value;
                OnPropertyChanged();
            }
        }

        public double AxisMinY
        {
            get => axisMinY;
            set
            {
                axisMinY = value;
                OnPropertyChanged();
            }
        }

        public double AxisUnitX
        {
            get => axisUnitX;
            set
            {
                axisUnitX = value;
            }
        }
        public double AxisUnitY
        {
            get => axisUnitY;
            set
            {
                axisUnitY = value;
            }
        }

        public double AxisStepX
        {
            get => axisStepX;
            set
            {
                axisStepX = value;
            }
        }

        public double AxisStepY
        {
            get => axisStepY;
            set
            {
                axisStepY = value;
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
