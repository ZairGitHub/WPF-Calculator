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

        private double _input;
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
            _input = 0;
            UpdateOutputText();
            ClearOperation();
            ClearHistoryText();
        }

        private void UpdateOutputText() => _textOutput.Text = _input.ToString();

        private void ClearOperation() => _operation = null;
        
        private void ClearHistoryText() => _textHistory.Text = null;

        private void PrepareNewEquationEnvironment(object buttonClick)
        {
            if (buttonClick is double)
            {
                ClearAll();
            }
            else
            {
                ClearHistoryText();
            }
            _isNewEquation = false;
        }

        private void Button_Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            double number = Convert.ToDouble(button.Content.ToString());

            if (_isNewEquation)
            {
                PrepareNewEquationEnvironment(number);
            }
            _input = (_input * 10) + number;
            UpdateOutputText();
        }

        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _operation = button.Content.ToString();

            if (_isNewEquation)
            {
                PrepareNewEquationEnvironment(_operation);
            }
            _listHistory.Add(_input);
            _listHistory.Add(_operation);
            _textHistory.Text += $"{_input} {_operation} ";

            if (_operation != "=")
            {
                _input = 0;
                ClearOperation();
                UpdateOutputText();
            }
            else
            {
                CalculateSum(_listHistory);
            }
        }

        private void CalculateSum(List<object> list)
        {
            _input = Convert.ToDouble(list[0]);
            string operation = Convert.ToString(list[1]);
            double number;
            bool hasException = false;
            for (int i = 2; i < list.Count; i++)
            {
                if (hasException)
                {
                    list.Clear();
                    _input = 0;
                    break;
                }

                if (i % 2 == 0)
                {
                    number = Convert.ToDouble(list[i]);
                    switch (operation)
                    {
                        case "+":
                            _input = Calculator.Add(_input, number);
                            break;
                        case "-":
                            _input = Calculator.Subtract(_input, number);
                            break;
                        case "*":
                            _input = Calculator.Multiply(_input, number);
                            break;
                        case "/":
                            try
                            {
                                _input = Calculator.Divide(_input, number);
                            }
                            catch (DivideByZeroException e)
                            {
                                _textHistory.Text = e.Message;
                                hasException = true;
                            }
                            break;
                    }
                }
                else
                {
                    operation = Convert.ToString(list[i]);
                }
            }
            _isNewEquation = true;
            UpdateOutputText();
        }

        private void Button_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void Button_ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            _input = 0;
            UpdateOutputText();
        }

        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (_input >= 10 || _input <= -10)
            {
                string inputString = _input.ToString();
                inputString = inputString.Substring(0, inputString.Length - 1);
                _input = Convert.ToDouble(inputString);
            }
            else
            {
                _input = 0;
            }
            UpdateOutputText();
        }

        private void Button_SignChange_Click(object sender, RoutedEventArgs e)
        {
            _input = -_input;
            UpdateOutputText();
        }
    }
}
