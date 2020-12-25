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

namespace WPFCalculator
{
    public partial class MainWindow : Window
    {
        private readonly List<object> _listHistory = new List<object>();

        private double _input;
        private double _sum;
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
            _sum = 0;

            UpdateOutputText();
            ClearOperation();
            ClearHistoryText();
        }

        private void UpdateOutputText() => TextOutput.Text = _input.ToString();

        private void ClearOperation() => _operation = null;
        
        private void ClearHistoryText() => TextHistory.Text = null;
        
        private void AddToListHistory(double number, string operation)
        {
            _listHistory.Add(number);
            _listHistory.Add(operation);

            TextHistory.Text += $"{number} {operation} ";
        }

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
            AddToListHistory(_input, _operation);

            if (_operation != "=")
            {
                _input = 0;
                ClearOperation();
                UpdateOutputText();
            }
            else
            {
                if (_listHistory.Count < 3)
                {
                    _sum = 0;
                }
                else
                {
                    _sum = Convert.ToDouble(_listHistory[0]);

                    double number;
                    string operation = null;
                    for (int i = 1; i < _listHistory.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            operation = Convert.ToString(_listHistory[i]);
                        }
                        else
                        {
                            number = Convert.ToDouble(_listHistory[i]);

                            switch (operation)
                            {
                                case "+":
                                    _sum += number;
                                    break;
                                case "-":
                                    _sum -= number;
                                    break;
                                case "*":
                                    _sum *= number;
                                    break;
                                case "/":
                                    _sum /= number;
                                    break;
                            }
                        }
                    }
                    _isNewEquation = true;
                    _input = _sum;
                    UpdateOutputText();
                }
            }
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
