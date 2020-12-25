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
        string _operation;

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
            _operation = string.Empty;
            SetOutputText(_input);
            ClearHistoryText();
        }

        private void SetOutputText<T>(T text)
        {
            TextOutput.Text = text.ToString();
        }

        private void AddToListHistory(object obj)
        {
            _listHistory.Add(obj);
            TextHistory.Text += obj.ToString();
        }

        private void ClearHistoryText()
        {
            TextHistory.Text = string.Empty;
        }

        private void Button_Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            double number = Convert.ToDouble(button.Content.ToString());
            _input = (_input * 10) + number;
            SetOutputText(_input);
        }

        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            //_listHistory.Add(_input);
            AddToListHistory(_input);

            Button button = (Button)sender;
            _operation = button.Content.ToString();
            if (_operation != "=")
            {
                //_listHistory.Add(_operation);
                AddToListHistory(_operation);

                _input = 0;
                _operation = string.Empty;
                SetOutputText(_input);
            }
            else
            {
                AddToListHistory(_operation);
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
                    _input = 0;
                    _listHistory.Clear();
                    SetOutputText(_sum);
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
            SetOutputText(_input);
        }

        private void Button_Backspace_Click(object sender, RoutedEventArgs e)
        {
            string inputString = _input.ToString();
            if (inputString.Length > 1)
            {
                inputString = inputString.Substring(0, inputString.Length - 1);
                _input = Convert.ToDouble(inputString);
                SetOutputText(_input);
            }
        }

        private void Button_SignChange_Click(object sender, RoutedEventArgs e)
        {
            _input = -_input;
            SetOutputText(_input);
        }
    }
}
