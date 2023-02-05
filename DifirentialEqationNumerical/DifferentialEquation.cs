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
	    public static IEnumerable<IPoint> CalculateDifferentialEquation_EnumerableReturn_Eiler(string expression, double x0, double y0, double h, int stepAmounts, Func<string, double, double, double> calculateExpression)
		{

			var resultMass = new System.Collections.ObjectModel.ObservableCollection<IPoint>();

			for (int i = 0; i < stepAmounts; i++)
				resultMass.Add(new Point(x0 + i * h, 0));

			resultMass[0].Y = y0;

			for (int i = 1; i < stepAmounts; i++)
				resultMass[i].Y = resultMass[i - 1].Y + h *
					calculateExpression(expression, resultMass[i - 1].X, resultMass[i - 1].Y);

			return resultMass;
		}

		public static IEnumerable<IPoint> CalculateDifferentialEquation_EnumerableReturn_ImpEiler(string expression, double x0, double y0, double h, int stepAmounts, Func<string, double, double, double> calculateExpression)
		{
			var resultMass = new System.Collections.ObjectModel.ObservableCollection<IPoint>();

			for (int i = 0; i < stepAmounts; i++)
				resultMass.Add(new Point(x0 + i * h, 0));

			resultMass[0].Y = y0;

			for (int i = 1; i < stepAmounts; i++)
				resultMass[i].Y = resultMass[i - 1].Y + h * 
					calculateExpression(expression, resultMass[i - 1].X + (h / 2) *
					calculateExpression(expression, resultMass[i - 1].X, resultMass[i - 1].Y),
					resultMass[i - 1].Y + (h / 2));

			return resultMass;
		}

		public static IEnumerable<IPoint> CalculateDifferentialEquation_EnumerableReturn_RyngeKytte(string expression, double x0, double y0,  double h, int stepAmounts, Func<string, double, double, double> calculateExpression)
		{
			var resultMass = new System.Collections.ObjectModel.ObservableCollection<IPoint>();

			for (int i = 0; i < stepAmounts; i++)
				resultMass.Add(new Point(x0 + i * h, 0));


			double k1, k2, k3, k4;

			resultMass[0].Y = y0;
			for (int i = 1; i < stepAmounts; i++)
			{
				k1 = calculateExpression(expression, resultMass[i - 1].X, resultMass[i - 1].Y);
				k2 = calculateExpression(expression, resultMass[i - 1].X + h * k1 / 2, resultMass[i - 1].Y + h / 2);
				k3 = calculateExpression(expression, resultMass[i - 1].X + h * 0.5 * k2, resultMass[i - 1].Y + 0.5 * h);
				k4 = calculateExpression(expression, resultMass[i - 1].X + h * k3, resultMass[i - 1].Y + h);
				resultMass[i].Y = resultMass[i - 1].Y + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
			}

			return resultMass;
		}

		public static async Task<IEnumerable<IPoint>> CalculateDifferentialEquation_EnumerableReturn_Eiler_Async(string expression, double x0, double y0, double h, int stepAmounts, Func<string, double, double, double> calculateExpression)
		{
			return await Task.Run(() => CalculateDifferentialEquation_EnumerableReturn_Eiler(expression, x0, y0, h, stepAmounts, calculateExpression));
		}

		public static async Task<IEnumerable<IPoint>> CalculateDifferentialEquation_EnumerableReturn_ImpEiler_Async(string expression, double x0, double y0, double h, int stepAmounts, Func<string, double, double, double> calculateExpression)
		{
			return await Task.Run(() => CalculateDifferentialEquation_EnumerableReturn_ImpEiler(expression, x0, y0, h, stepAmounts, calculateExpression));
		}

		public static async Task<IEnumerable<IPoint>> CalculateDifferentialEquation_EnumerableReturn_RyngeKytte_Async(string expression, double x0, double y0, double h, int stepAmounts, Func<string, double, double, double> calculateExpression)
        {
		    return await Task.Run(() => CalculateDifferentialEquation_EnumerableReturn_RyngeKytte(expression,x0,y0,h,stepAmounts,calculateExpression));
		}
	}
}
