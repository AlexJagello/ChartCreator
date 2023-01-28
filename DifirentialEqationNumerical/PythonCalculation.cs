using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifirentialEqationNumerical
{
    public static class PythonCalculationClass
    {
        private static readonly ScriptEngine engine;
        private static readonly ScriptScope scope;

        static PythonCalculationClass()
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();
        }

        public static double PythonCalculation(string expression, double x)
        {
            scope.SetVariable("x", x);
            engine.Execute("from math import *\n" + expression, scope);
            dynamic res = scope.GetVariable("y");
            return Convert.ToDouble(res);
        }
    }
}
