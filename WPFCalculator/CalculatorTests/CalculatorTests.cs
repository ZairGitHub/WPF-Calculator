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
            double a = It.IsAny<double>();
            double b = It.IsAny<double>();

            var result = Calculator.Add(a, b);

            Assert.Equal(a + b, result);
        }

        [Fact]
        public void Subtract_ReturnsAMinusB()
        {
            double a = It.IsAny<double>();
            double b = It.IsAny<double>();

            var result = Calculator.Subtract(a, b);

            Assert.Equal(a - b, result);
        }

        [Fact]
        public void Multiply_ReturnsAMultipliedByB()
        {
            double a = It.IsAny<double>();
            double b = It.IsAny<double>();

            var result = Calculator.Multiply(a, b);

            Assert.Equal(a * b, result);
        }

        [Fact]
        public void Divide_BIsZero_ThrowsDivideByZeroExceptionWithCustomMessage()
        {
            double a = It.IsAny<double>();
            double b = 0;

            var result = Assert.Throws<DivideByZeroException>(() =>
                Calculator.Divide(a, b));

            Assert.Equal("Cannot divide by zero", result.Message);
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-double.Epsilon)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MaxValue)]
        public void Divide_BIsNotZero_ReturnsADividedByB(double b)
        {
            double a = It.IsAny<double>();

            var result = Calculator.Divide(a, b);

            Assert.Equal(a / b, result);
        }

        [Fact]
        public void Modulo_BIsZero_ThrowsDivideByZeroExceptionWithCustomMessage()
        {
            double a = It.IsAny<double>();
            double b = 0;

            var result = Assert.Throws<DivideByZeroException>(() =>
                Calculator.Modulo(a, b));

            Assert.Equal("Cannot divide by zero", result.Message);
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-double.Epsilon)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MaxValue)]
        public void Modulo_BIsNotZero_ReturnsAModuloB(double b)
        {
            double a = It.IsAny<double>();

            var result = Calculator.Modulo(a, b);

            Assert.Equal(a % b, result);
        }

        [Fact]
        public void Reciprocal_NumberIsZero_ThrowsDivideByZeroExceptionWithCustomMessage()
        {
            double a = 0;

            var result = Assert.Throws<DivideByZeroException>(() =>
                Calculator.Reciprocal(a));

            Assert.Equal("Cannot divide by zero", result.Message);
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-double.Epsilon)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MaxValue)]
        public void Reciprocal_NumberIsNotZero_Returns1DividedByNumber(double a)
        {
            var result = Calculator.Reciprocal(a);

            Assert.Equal(1 / a, result);
        }

        [Fact]
        public void Exponent_ReturnsAWithExponentOfB()
        {
            double a = It.IsAny<double>();
            double b = It.IsAny<double>();

            var result = Calculator.Exponent(a, b);

            Assert.Equal(Math.Pow(a, b), result);
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-double.Epsilon)]
        public void SquareRoot_NumberIsNegative_ThrowsArgumentExceptionWithCustomMessage(double a)
        {
            var result = Assert.Throws<ArgumentException>(() =>
                Calculator.SquareRoot(a));

            Assert.Equal("Cannot square root negative numbers", result.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MaxValue)]
        public void SquareRoot_NumberIsZeroOrPositive_ReturnsSquareRootOfNumber(double a)
        {
            var result = Calculator.SquareRoot(a);

            Assert.Equal(Math.Sqrt(a), result);
        }
    }
}
