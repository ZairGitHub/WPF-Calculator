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
            _operation = null;
            TextOutput.Text = "0";
        }

        private void SetOutputText(string text)
        {
            TextOutput.Text = text;
        }

        private void Button_Number(object sender, RoutedEventArgs e)
        {
            double result;
            Button button = (Button)sender;
            if (double.TryParse(button.Content.ToString(), out result))
            {
                if (_operation == null)
                {
                    _number1 = (_number1 * 10) + result;
                }
                else
                {
                    _operation = null;
                    _number2 = _number1;
                    _number1 = (_number1 * 10) + result;
                }
                SetOutputText(_number1.ToString());
            }
        }

        private void Button_Operation(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SetOutputText(button.Content.ToString());
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }
    }
}
