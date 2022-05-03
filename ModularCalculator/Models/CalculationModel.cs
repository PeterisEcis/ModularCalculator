using System;

namespace ModularCalculator.Models
{
    public class CalculationModel
    {
        private string result;
        private AssemblyManager manager;

        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }

        public CalculationModel(string firstOperand, string secondOperand, string operation)
        {
            ValidateOperand(firstOperand);
            ValidateOperand(secondOperand);
            ValidateOperation(operation);

            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
            result = string.Empty;
            manager = new AssemblyManager();
        }

        public CalculationModel(string firstOperand, string operation)
        {
            ValidateOperand(firstOperand);
            ValidateOperation(operation);

            FirstOperand = firstOperand;
            SecondOperand = string.Empty;
            Operation = operation;
            result = string.Empty;
            manager = new AssemblyManager();

        }

        public CalculationModel()
        {
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
            manager = new AssemblyManager();
        }

        public void UpdateAssembly()
        {
            manager.ReloadAssembly();
        }

        public void CalculateResult()
        {
            ValidateData();

            try
            {
                switch (Operation)
                {
                    case ("+"):
                        result = manager.Add(Convert.ToDouble(FirstOperand), Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("-"):
                        result = manager.Subtract(Convert.ToDouble(FirstOperand), Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("*"):
                        result = manager.Multiply(Convert.ToDouble(FirstOperand), Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("/"):
                        result = manager.Divide(Convert.ToDouble(FirstOperand), Convert.ToDouble(SecondOperand)).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                result = "Error whilst calculating";
                throw;
            }
        }

        private void ValidateOperand(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch (Exception)
            {
                result = "Invalid number: " + operand;
                throw;
            }
        }

        private void ValidateOperation(string operation)
        {
            switch (operation)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                    break;
                default:
                    result = "Unknown operation: " + operation;
                    throw new ArgumentException("Unknown Operation: " + operation, "operation");
            }
        }

        private void ValidateData()
        {
            switch (Operation)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                    ValidateOperand(FirstOperand);
                    ValidateOperand(SecondOperand);
                    break;
                default:
                    result = "Unknown operation: " + Operation;
                    throw new ArgumentException("Unknown Operation: " + Operation, "operation");
            }
        }
    }
}
