using ModularCalculator.Commands;
using System.Windows.Input;
using ModularCalculator.Models;
using System;

namespace ModularCalculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CalculationModel calculation;
        private DelegateCommand<string> digitButtonPressCommand;
        private DelegateCommand<string> operationButtonPressCommand;

        private string lastOperation;
        private bool newDisplayRequired = false;
        private string fullExpression;
        private string display;

        public MainWindowViewModel()
        {
            this.calculation = new CalculationModel();
            this.display = "0";
            this.lastOperation = string.Empty;
        }

        public string Display
        {
            get { return this.display; }
            set
            {
                this.display = value;
                OnPropertyChanged(nameof(Display));
            }
        }

        public string FirstOperand
        {
            get { return calculation.FirstOperand; }
            set { calculation.FirstOperand = value; }
        }

        public string SecondOperand
        {
            get { return calculation.SecondOperand; }
            set { calculation.SecondOperand = value; }
        }

        public string Operation
        {
            get { return calculation.Operation; }
            set { calculation.Operation = value; }
        }

        public string LastOperation
        {
            get { return lastOperation; }
            set { lastOperation = value; }
        }
        public string Result
        {
            get { return calculation.Result; }
        }

        public string FullExpression
        {
            get { return fullExpression; }
            set
            {
                fullExpression = value;
                OnPropertyChanged("FullExpression");
            }
        }

        public ICommand OperationButtonPressCommand
        {
            get
            {
                if (operationButtonPressCommand == null)
                {
                    operationButtonPressCommand = new DelegateCommand<string>(
                        OperationButtonPress, CanOperationButtonPress);
                }
                return operationButtonPressCommand;
            }
        }

        private static bool CanOperationButtonPress(string number)
        {
            return true;
        }

        public ICommand DigitButtonPressCommand
        {
            get
            {
                if (digitButtonPressCommand == null)
                {
                    digitButtonPressCommand = new DelegateCommand<string>(
                        DigitButtonPress, CanDigitButtonPress);
                }
                return digitButtonPressCommand;
            }
        }

        private static bool CanDigitButtonPress(string button)
        {
            return true;
        }


        //deals with button inputs and sorts out the display accordingly
        public void DigitButtonPress(string button)
        {
            switch (button)
            {
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    FullExpression = string.Empty;
                    break;
                case "Del":
                    if (display.Length > 1)
                        Display = display.Substring(0, display.Length - 1);
                    else Display = "0";
                    break;
                case "+/-":
                    if (display.Contains("-") || display == "0")
                    {
                        Display = display.Remove(display.IndexOf("-"), 1);
                    }
                    else Display = "-" + display;
                    break;
                case ".":
                    if (newDisplayRequired)
                    {
                        Display = "0.";
                    }
                    else
                    {
                        if (!display.Contains("."))
                        {
                            Display = display + ".";
                        }
                    }
                    break;
                default:
                    if (display == "0" || newDisplayRequired)
                        Display = button;
                    else
                        Display = display + button;
                    break;
            }
            newDisplayRequired = false;
        }

        public void OperationButtonPress(string operation)
        {
            try
            {
                if (FirstOperand == string.Empty || LastOperation == "=")
                {
                    FirstOperand = display;
                    LastOperation = operation;
                }
                else
                {
                    SecondOperand = display;
                    Operation = lastOperation;
                    calculation.CalculateResult();

                    FullExpression = Math.Round(Convert.ToDouble(FirstOperand), 10) + " " + Operation + " "
                                    + Math.Round(Convert.ToDouble(SecondOperand), 10) + " =\n"
                                    + Math.Round(Convert.ToDouble(Result), 10);

                    LastOperation = operation;
                    Display = Result;
                    FirstOperand = display;
                }
                newDisplayRequired = true;
            }
            catch (Exception ex)
            {
                Display = Result == string.Empty ? "Error - see event log" : Result;
                //LogExceptionInformation(ex);
            }
        }
    }
}
