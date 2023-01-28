using DifirentialEqationNumerical.Models;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifirentialEqationNumerical
{
    public static class DifferentialEquation
    {

        public static IEnumerable<IPoint> CalculateDifferentialEquation_EnumerableReturn(string expression, double x0, double step, int stepAmounts)
        {
            var resultMass = new System.Collections.ObjectModel.ObservableCollection<IPoint>();


            return resultMass;
        }


		static void integrfunEiler(string expression,double x0, double y0, int n, double[] y1, double h)
		{
			double[] x1 = new double[n];
			double t = 0;
			for (int i = 0; i < n; i++)
				x1[i] = x0 - i * h;

			y1[0] = y0;

			for (int i = 1; i <= n; i++)
			{
				t += h;
				y1[i] = y1[i - 1] + h* PythonCalculationClass.PythonCalculation(expression, x1[i - 1]); //* fun2(y1[i - 1], x1[i - 1], t * y1[i - 1], k, m);
			}

			t = 0;
			for (int i = 0; i < n; i++)
			{
				t += h;
				y1[i] = y1[i] * t;
			}
		}


	}
}
