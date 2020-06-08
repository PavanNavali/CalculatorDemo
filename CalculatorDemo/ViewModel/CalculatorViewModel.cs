using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CalculatorDemo
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        #region Constrctor
        public CalculatorViewModel()
        {
            AppendCommand = new RelayCommand(AppendMethod, null);
            OperationCommand = new RelayCommand(AppendOperandMethod, null);
            CancelCommand = new RelayCommand(CancelMethod, null);
            CalculateCommand = new RelayCommand(CalculateMethod, null);
        }

        #endregion

        #region InotigyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties and Commands
        /// <summary>
        /// 
        /// </summary>
        public string CurrentValue
        {
            get
            {
                return currentValue;
            }

            set
            {
                currentValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentValue"));
            }
        }

        /// <summary>
        /// Holds the result of the expression which is evaluated.
        /// </summary>
        public string CurrentResult
        {
            get
            {
                return "= " + currentResult;
            }

            set
            {
                currentResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentResult"));
            }
        }

        /// <summary>
        /// Displays the entire expression, which includes numbers and the operation performed.
        /// </summary>
        public string FullExpression
        {
            get
            {
                return fullExpression;
            }

            set
            {
                fullExpression = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullExpression"));
            }
        }

        /// <summary>
        /// Append the numbers as an when they are clicked, which are passed as command parameter.
        /// </summary>
        public ICommand AppendCommand
        {
            get
            {
                return appendCommand;
            }

            set
            {
                if (appendCommand == null)
                {
                    appendCommand = value;
                }
            }
        }

        /// <summary>
        /// This command appends the operation that need to be performed/
        /// </summary>
        public ICommand OperationCommand
        {
            get
            {
                return operationCommand;
            }

            set
            {
                if (operationCommand == null)
                {
                    operationCommand = value;
                }

            }
        }

        /// <summary>
        /// Cancels the whole operation and resets all the variables.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand;
            }

            set
            {
                cancelCommand = value;
            }
        }

        /// <summary>
        /// Calculates the result.
        /// </summary>
        public ICommand CalculateCommand
        {
            get
            {
                return calculateCommand;
            }

            set
            {
                calculateCommand = value;
            }
        }
        #endregion

        #region Private Methods
        private void AppendMethod(object parameter)
        {
            if (currentOperator == "/" && parameter.ToString() == "0")
            {
                MessageBox.Show("Divsion by Zero is not permitted.");
                return;
            }
            CurrentValue += parameter.ToString();
            FullExpression += parameter.ToString();
        }

        private void CancelMethod(object parameter)
        {
            CurrentValue = string.Empty;
            CurrentResult = string.Empty;
            FullExpression = string.Empty;
            result = 0;
        }

        private void CalculateMethod(object parameter)
        {
            if (!string.IsNullOrEmpty(CurrentValue))
            {
                result = Calculate();
                CurrentResult = result.ToString();
                CurrentValue = result.ToString();
                currentOperator = null;
            }
        }

        private void AppendOperandMethod(object parameter)
        {
            FullExpression += parameter.ToString();
            result = Calculate();
            CurrentResult = result.ToString();
            CurrentValue = result.ToString();
            CurrentValue = string.Empty;
            currentOperator = parameter.ToString();
        }

        private long Calculate()
        {
            long val = 0, num2 = long.Parse(CurrentValue);
            if (currentOperator == null)
            {
                return num2;
            }
            switch (currentOperator)
            {
                case "+":
                    val = result + num2;
                    break;
                case "-":
                    val = result - num2;
                    break;
                case "*":
                    val = result * num2;
                    break;
                case "/":
                    val = result / num2;
                    break;
            }
            return val;
        }
        #endregion

        #region Private Members

        private long result;
        private string currentOperator;
        private string currentValue;
        private string fullExpression;
        private string currentResult;

        private ICommand appendCommand;
        private ICommand operationCommand;
        private ICommand cancelCommand;
        private ICommand calculateCommand;

        #endregion
    }

}
