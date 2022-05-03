using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModularCalculator.Models
{
    public class AssemblyManager
    {
        private Assembly assembly;
        private Type type;

        public AssemblyManager()
        {
            assembly = Assembly.Load(Constants.AssemblyFileName);
            type = assembly.GetType(Constants.ClassName);
        }
        
        public void ReloadAssembly()
        {
            assembly = Assembly.Load(Constants.AssemblyFileName);
            type = assembly.GetType(Constants.ClassName);

        }

        public double? Add(double num1, double num2)
        {
            var info = type.GetMethod(Constants.AddMethod);
            object[] parameters = { num1, num2 };
            var result = info.Invoke(null, parameters).ToString();
            if(double.TryParse(result, out double resultDouble))
            {
                return resultDouble;
            }
            else
            {
                return null;
            }
        }

        public double? Subtract(double num1, double num2)
        {
            var info = type.GetMethod(Constants.SubtractMethod);
            object[] parameters = { num1, num2 };
            var result = info.Invoke(null, parameters).ToString();
            if (double.TryParse(result, out double resultDouble))
            {
                return resultDouble;
            }
            else
            {
                return null;
            }
        }

        public double? Multiply(double num1, double num2)
        {
            var info = type.GetMethod(Constants.MultiplyMethod);
            object[] parameters = { num1, num2 };
            var result = info.Invoke(null, parameters).ToString();
            if (double.TryParse(result, out double resultDouble))
            {
                return resultDouble;
            }
            else
            {
                return null;
            }
        }

        public double? Divide(double num1, double num2)
        {
            var info = type.GetMethod(Constants.DivideMethod);
            object[] parameters = { num1, num2 };
            var result = info.Invoke(null, parameters).ToString();
            if (double.TryParse(result, out double resultDouble))
            {
                return resultDouble;
            }
            else
            {
                return null;
            }
        }
    }
}
