using System;

namespace CalculatorApp
{
    public static class Calculator
    {
        public static double Add(double a, double b) => a + b;

        public static double Subtract(double a, double b) => a - b;

        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return a / b;
        }

        public static double Modulo(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return a % b;
        }

        public static double Percentage(double a)
        {
            if (a == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return 1 / a;
        }
        
        public static double Exponent(double a)
        {
            return Math.Pow(a, 2);
        }

        public static double SquareRoot(double a)
        {
            if (a < 0)
            {
                throw new ArgumentException("Can not square root negative numbers");
            }
            return Math.Sqrt(a);
        }
            
    }
}
