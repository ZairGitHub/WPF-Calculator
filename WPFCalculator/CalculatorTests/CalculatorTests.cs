using System;
using CalculatorApp;
using Moq;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsAPlusB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Add(a, b);

            Assert.Equal(a + b, result);
        }

        [Fact]
        public void Subtract_ReturnsAMinusB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Subtract(a, b);

            Assert.Equal(a - b, result);
        }

        [Fact]
        public void Multiply_ReturnsAMultipliedByB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Multiply(a, b);

            Assert.Equal(a * b, result);
        }

        [Fact]
        public void Divide_BIsZero_ThrowsDivideByZeroExceptionWithCustomMessage()
        {
            double a = It.IsAny<int>();
            double b = 0;

            var result = Assert.Throws<DivideByZeroException>(() =>
                Calculator.Divide(a, b));

            Assert.Equal("Cannot divide by zero", result.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        public void Divide_BIsNotZero_ReturnsADividedByB(int b)
        {
            double a = It.IsAny<int>();

            var result = Calculator.Divide(a, b);

            Assert.Equal(a / b, result);
        }

        [Fact]
        public void Modulo_BIsZero_ThrowsDivideByZeroExceptionWithCustomMessage()
        {
            double a = It.IsAny<int>();
            double b = 0;

            var result = Assert.Throws<DivideByZeroException>(() =>
                Calculator.Modulo(a, b));

            Assert.Equal("Cannot divide by zero", result.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        public void Modulo_BIsNotZero_ReturnsAModuloB(int b)
        {
            double a = It.IsAny<int>();

            var result = Calculator.Modulo(a, b);

            Assert.Equal(a % b, result);
        }

        [InlineData(-1)]
        [InlineData(1)]
        [Theory]
        public void Percentage_NumberIsNotZero_ReturnsNumberDividedBy1(double a)
        {
            var result = Calculator.Percentage(a);

            Assert.Equal(1 / a, result);
        }

        [Fact]
        public void Percentage_NumberIsZero_ThrowsArgumentExceptionWithCustomMessage()
        {
            double a = 0;

            var result = Assert.Throws<DivideByZeroException>(() =>
                Calculator.Percentage(a));

            Assert.Equal("Cannot divide by zero", result.Message);
        }

        [Fact]
        public void Exponent_ReturnsNumberWithExponentOf2()
        {
            double a  = It.IsAny<int>();

            var result = Calculator.Exponent(a);

            Assert.Equal(Math.Pow(a, 2), result);
        }

        [Fact]
        public void SquareRoot_NegativeNumber_ThrowsArgumentExceptionWithCustomMessage()
        {
            double a = -1;

            var result = Assert.Throws<ArgumentException>(() =>
                Calculator.SquareRoot(a));

            Assert.Equal("Can not square root negative numbers", result.Message);
        }

        [Fact]
        public void SquareRoot_PositiveNumber_ReturnsSquareRootOfNumber()
        {
            double a = It.IsAny<int>();

            var result = Calculator.SquareRoot(a);

            Assert.Equal(Math.Sqrt(a), result);
        }
    }
}