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
        private double _number1;
        private double _number2;
        string _operation;

        public MainWindow()
        {
            InitializeComponent();
            ResetAll();
        }

        private void ResetAll()
        {
            _number1 = 0;
            _number2 = 0;
            _operation = string.Empty;
            SetOutputText(_number1);
        }

        private void SetOutputText<T>(T text)
        {
            TextOutput.Text = text.ToString();
        }

        private void Button_Number(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (double.TryParse(button.Content.ToString(), out double result))
            {
                if (_operation == string.Empty)
                {
                    _number1 = (_number1 * 10) + result;
                }
                else
                {
                    _operation = string.Empty;
                    _number2 = _number1;
                    _number1 = (_number1 * 10) + result;
                }
                SetOutputText(_number1);
            }
        }

        private void Button_Operation(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _operation = button.Content.ToString();
            switch (_operation)
            {
                case "+":
                    _number2 += _number1;
                    break;
                case "-":
                    _number2 -= _number1;
                    break;
                case "*":
                    SetOutputText(_operation);
                    break;
                case "/":
                    SetOutputText(_operation);
                    break;
                case "=":
                    break;
            }
            _number1 = 0;
            _operation = string.Empty;
            SetOutputText(_number2);
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }
    }
}
