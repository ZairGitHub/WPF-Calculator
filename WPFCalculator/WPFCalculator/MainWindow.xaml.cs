using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CalculatorApp;

namespace WPFCalculator
{
    public partial class MainWindow : Window
    {
        private readonly List<object> _listHistory = new List<object>();

        private string _input;
        private string _operation;
        private bool _isNewEquation;

        public MainWindow()
        {
            InitializeComponent();
            ClearAll();
        }

        private void ClearAll()
        {
            _listHistory.Clear();
            ResetInput();
            UpdateOutputText();
            ClearOperation();
            ClearHistoryText();
        }

        private void ResetInput() => _input = "0";

        private void UpdateOutputText() => _textOutput.Text = _input;

        private void ClearOperation() => _operation = null;
        
        private void ClearHistoryText() => _textHistory.Text = null;

        private void Button_SignChange_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(_input) != 0)
            {
                if (_input[0] == '-')
                {
                    _input = _input.Substring(1, _input.Length - 1);
                }
                else
                {
                    _input = "-" + _input;
                }
                UpdateOutputText();
            }
        }

        private void Button_Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!_input.Contains("."))
            {
                _input += ".";
            }
        }

        private void Button_Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            double number = Convert.ToDouble(button.Content.ToString());

            if (_isNewEquation)
            {
                ClearHistoryForNewEquation(number);
            }

            if (_input == "0")
            {
                _input = null;
            }

            _input += number;
            UpdateOutputText();
        }

        private void ClearHistoryForNewEquation(object buttonContent)
        {
            if (buttonContent is double)
            {
                ClearAll();
            }
            else
            {
                ClearHistoryText();
            }
            _isNewEquation = false;
        }

        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _operation = button.Content.ToString();

            if (_isNewEquation)
            {
                ClearHistoryForNewEquation(_operation);
            }

            if (_operation.Contains("1/"))
            {
                _operation = "1/";
                _isNewEquation = true;
            }
            else if (_operation.Contains("^"))
            {
                _operation = "^";
            }
            else if (_operation.Contains("Sqrt"))
            {
                _operation = "Sqrt";
                _isNewEquation = true;
            }
            if (_isNewEquation)
            {
                _textHistory.Text += $"{_operation}({_input}) ";
            }
            else
            {
                _textHistory.Text += $"{_input} {_operation} ";
            }

            _listHistory.Add(_input);
            _listHistory.Add(_operation);
            CalculateSum(_listHistory);

            if (_operation == "=")
            {
                _isNewEquation = true;
            }
            else
            {
                ResetInput();
                ClearOperation();
            }
        }

        private void CalculateSum(List<object> listHistory)
        {
            double sum = Convert.ToDouble(listHistory[0]);
            string operation = null;
            double number;
            bool hasException = false;
            bool isSingleOperation = false;
            for (int i = 1; i < listHistory.Count; i++)
            {
                if (hasException)
                {
                    listHistory.Clear();
                    sum = 0;
                    break;
                }

                if (isSingleOperation)
                {
                    isSingleOperation = false;
                    continue;
                }

                if (i % 2 != 0)
                {
                    operation = Convert.ToString(listHistory[i]);
                    if (operation == "1/")
                    {
                        try
                        {
                            sum = Calculator.Reciprocal(sum);
                        }
                        catch (DivideByZeroException ex)
                        {
                            _textHistory.Text = ex.Message;
                            hasException = true;
                        }
                        isSingleOperation = true;
                    }
                    else if (operation == "Sqrt")
                    {
                        try
                        {
                            sum = Calculator.SquareRoot(sum);
                        }
                        catch (ArgumentException ex)
                        {
                            _textHistory.Text = ex.Message;
                            hasException = true;
                        }
                        isSingleOperation = true;
                    }
                }
                else
                {
                    number = Convert.ToDouble(listHistory[i]);
                    switch (operation)
                    {
                        case "+":
                            sum = Calculator.Add(sum, number);
                            break;
                        case "-":
                            sum = Calculator.Subtract(sum, number);
                            break;
                        case "*":
                            sum = Calculator.Multiply(sum, number);
                            break;
                        case "/":
                            try
                            {
                                sum = Calculator.Divide(sum, number);
                            }
                            catch (DivideByZeroException ex)
                            {
                                _textHistory.Text = ex.Message;
                                hasException = true;
                            }
                            break;
                        case "%":
                            try
                            {
                                sum = Calculator.Modulo(sum, number);
                            }
                            catch (DivideByZeroException ex)
                            {
                                _textHistory.Text = ex.Message;
                                hasException = true;
                            }
                            break;
                        case "^":
                            sum = Calculator.Exponent(sum, number);
                            break;
                    }
                }
            }
            _input = sum.ToString();
            UpdateOutputText();
        }

        private void Button_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void Button_ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            ResetInput();
            UpdateOutputText();
        }

        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (_input.Length > 1)
            {
                _input = _input.Substring(0, _input.Length - 1);
            }
            else
            {
                ResetInput();
            }

            if (_input[0] == '-' && Convert.ToDouble(_input) == 0)
            {
                _input = _input.Substring(1, _input.Length - 1);
            }
            UpdateOutputText();
        }
    }
}
