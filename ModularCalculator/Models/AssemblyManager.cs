using System;
using System.Reflection;

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

        public string Add(double num1, double num2)
        {
            var info = type.GetMethod(Constants.AddMethod);
            object[] parameters = { num1, num2 };
            try
            {
                var result = info.Invoke(null, parameters);
                if (result != null)
                {
                    return result.ToString();
                }
                return "Error";
            }
            catch
            {
                return "Error";
            }
        }

        public string Subtract(double num1, double num2)
        {
            var info = type.GetMethod(Constants.SubtractMethod);
            object[] parameters = { num1, num2 };
            try
            {
                var result = info.Invoke(null, parameters);
                if (result != null)
                {
                    return result.ToString();
                }
                return "Error";
            }
            catch
            {
                return "Error";
            }
        }

        public string Multiply(double num1, double num2)
        {
            var info = type.GetMethod(Constants.MultiplyMethod);
            object[] parameters = { num1, num2 };
            try
            {
                var result = info.Invoke(null, parameters);
                if (result != null)
                {
                    return result.ToString();
                }
                return "Error";
            }
            catch
            {
                return "Error";
            }
        }

        public string Divide(double num1, double num2)
        {
            var info = type.GetMethod(Constants.DivideMethod);
            object[] parameters = { num1, num2 };
            try
            {
                var result = info.Invoke(null, parameters);
                if (result != null)
                {
                    return result.ToString();
                }
                return "Error";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
