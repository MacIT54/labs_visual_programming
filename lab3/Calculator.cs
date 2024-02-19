using System;
using System.ComponentModel;

namespace lab3;

public class Calculator : INotifyPropertyChanged
    {
        private string _currentInput = "";
        private double _currentResult = 0;
        private char? _lastOperator = null;
        private char? _previousOperator = null;
        private double _previousOperand = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentInput
        {
            get => _currentInput;
            private set
            {
                _currentInput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInput)));
            }
        }

        public Calculator()
        {
        }

        public void AppendInput(char input)
        {
            _currentInput += input;
        }

        public void HandleNumberInput(char number)
        {
            AppendInput(number);
            CurrentInput = _currentInput;
        }

        public void HandleOperatorInput(char op)
        {
            // Проверяем, если есть непримененная операция, выполняем её
            if (_lastOperator != null)
            {
                PerformOperation();
            }

            // Если текущий результат не равен 0, значит, мы уже имеем первый операнд для новой операции
            if (_currentResult != 0)
            {
                // Присваиваем текущий результат как первый операнд для новой операции
                _currentInput = _currentResult.ToString();
            }
            else
            {
                // Если текущий результат равен 0, используем текущий ввод как первый операнд
                _currentResult = double.Parse(_currentInput);
            }

            // Устанавливаем новый оператор
            _lastOperator = op;

            // Очищаем текущий ввод для второго операнда
            CurrentInput = "";
        }

        public void HandleEqualsInput()
        {
            if (_lastOperator != null)
            {
                PerformOperation();
                _previousOperator = _lastOperator;
                _previousOperand = double.Parse(_currentInput);
                _lastOperator = null;
            }
            else if (_previousOperator != null)
            {
                PerformPreviousOperation(); // Выполнить предыдущую операцию
            }
            CurrentInput = _currentResult.ToString();
        }

        public void HandleClearInput()
        {
            CurrentInput = "";
            _currentResult = 0;
            _lastOperator = null;
        }

        public void HandleBackspaceInput()
        {
            if (_currentInput.Length > 0)
            {
                _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                CurrentInput = _currentInput;
            }
        }

        public void HandleModFunction()
        {
            if (_lastOperator != null)
            {
                PerformOperation();
            }
            _lastOperator = '%';
            _currentResult = double.Parse(_currentInput);
            CurrentInput = "";
        }

        public void HandlePowerFunction()
        {
            if (_lastOperator != null)
            {
                PerformOperation();
            }
            _lastOperator = 'x';
            _currentResult = double.Parse(_currentInput);
            CurrentInput = "";
        }

        public void HandleLogFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue) && inputValue > 0)
            {
                _currentResult = Math.Log10(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "Не число";
            }
        }

        public void HandleLnFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue) && inputValue > 0)
            {
                _currentResult = Math.Log(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "Не число";
            }
        }

        public void HandleSinFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                _currentResult = Math.Sin(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "Не число";
            }
        }

        public void HandleCosFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                _currentResult = Math.Cos(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "Не число";
            }
        }

        public void HandleTanFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                _currentResult = Math.Tan(inputValue);
                CurrentInput = _currentResult.ToString();
            }
            else
            {
                CurrentInput = "Не число";
            }
        }

        public void HandleFloorFunction()
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                double inputValue = double.Parse(_currentInput);
                _currentResult = Math.Floor(inputValue);
                CurrentInput = _currentResult.ToString();
            }
        }

        public void HandleCeilFunction()
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                double inputValue = double.Parse(_currentInput);
                _currentResult = Math.Ceiling(inputValue);
                CurrentInput = _currentResult.ToString();
            }

        }
        public void HandleFactorialFunction()
        {
            if (double.TryParse(_currentInput, out double inputValue))
            {
                if (inputValue >= 0 && Math.Floor(inputValue) == inputValue)
                {
                    double result = 1;
                    for (int i = 2; i <= inputValue; i++)
                    {
                        result *= i;
                    }
                    _currentResult = result;
                    CurrentInput = _currentResult.ToString();
                }
                else
                {
                    CurrentInput = "Ошибка";
                }
            }
            else
            {
                CurrentInput = "Ошибка";
            }
        }

        public void HandleCommaInput()
        {
            if (!_currentInput.Contains(","))
            {
                AppendInput(',');
                CurrentInput = _currentInput;
            }
        }

        private void PerformOperation()
        {
            if (_currentInput.Length > 0)
            {
                double operand = double.Parse(_currentInput);
                switch (_lastOperator)
                {
                    case '+':
                        _currentResult += operand;
                        break;
                    case '-':
                        _currentResult -= operand;
                        break;
                    case '*':
                        _currentResult *= operand;
                        break;
                    case '/':
                        _currentResult /= operand;
                        break;
                    case '%':
                        _currentResult %= operand;
                        break;
                    case 'x':
                        _currentResult = Math.Pow(_currentResult, operand);
                        break;
                }
            }
        }

        public void ProcessButtonClick(string buttonContent)
        {
            if (char.IsDigit(buttonContent[0]))
            {
                HandleNumberInput(buttonContent[0]);
            }
            else if (buttonContent == "+" || buttonContent == "-" || buttonContent == "*" || buttonContent == "/")
            {
                HandleOperatorInput(buttonContent[0]);
            }
            else if (buttonContent == "=")
            {
                HandleEqualsInput();
            }
            else if (buttonContent == "Clear")
            {
                HandleClearInput();
            }
            else if (buttonContent == "Del")
            {
                HandleBackspaceInput();
            }
            else if (buttonContent == "mod")
            {
                HandleModFunction();
            }
            else if (buttonContent == "x^y")
            {
                HandlePowerFunction();
            }
            else if (buttonContent == "lg")
            {
                HandleLogFunction();
            }
            else if (buttonContent == "ln")
            {
                HandleLnFunction();
            }
            else if (buttonContent == "sin")
            {
                HandleSinFunction();
            }
            else if (buttonContent == "cos")
            {
                HandleCosFunction();
            }
            else if (buttonContent == "tan")
            {
                HandleTanFunction();
            }
            else if (buttonContent == "floor")
            {
                HandleFloorFunction();
            }
            else if (buttonContent == "ceil")
            {
                HandleCeilFunction();
            }
            else if (buttonContent == "n!")
            {
                HandleFactorialFunction();
            }
            else if (buttonContent == ",")
            {
                HandleCommaInput();
            }
        }

        private void PerformPreviousOperation()
        {
            if (_previousOperator != null)
            {
                double operand = double.Parse(_currentInput);
                switch (_previousOperator)
                {
                    case '+':
                        _currentResult += _previousOperand;
                        break;
                    case '-':
                        _currentResult -= _previousOperand;
                        break;
                    case '*':
                        _currentResult *= _previousOperand;
                        break;
                    case '/':
                        _currentResult /= _previousOperand;
                        break;
                    case '%':
                        _currentResult %= _previousOperand;
                        break;
                    case 'x':
                        _currentResult = Math.Pow(_currentResult, _previousOperand);
                        break;
                }
            }
        }
    }