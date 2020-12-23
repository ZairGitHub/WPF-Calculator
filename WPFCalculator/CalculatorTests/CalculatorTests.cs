using CalculatorApp;
using NUnit.Framework;
using Moq;
using System;

namespace CalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_ReturnsAPlusB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Add(a, b);

            Assert.That(result, Is.EqualTo(a + b));
        }

        [Test]
        public void Subtract_ReturnsAMinusB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Subtract(a, b);

            Assert.That(result, Is.EqualTo(a - b));
        }

        [Test]
        public void Multiply_ReturnsAMultipliedByB()
        {
            double a = It.IsAny<int>();
            double b = It.IsAny<int>();

            var result = Calculator.Multiply(a, b);

            Assert.That(result, Is.EqualTo(a * b));
        }

        [Test]
        public void Divide_BIsZero_ThrowsDivideByZeroException()
        {
            double a = It.IsAny<int>();
            double b = 0;

            Assert.That(() => Calculator.Divide(a, b),
                Throws.Exception.TypeOf<DivideByZeroException>()
                .With.Message.EqualTo("Cannot divide by zero"));
        }

        [TestCase(-1)]
        [TestCase(1)]
        public void Divide_BIsNotZero_ReturnsADividedByB(int b)
        {
            double a = It.IsAny<int>();

            var result = Calculator.Divide(a, b);

            Assert.That(result, Is.EqualTo(a / b));
        }
    }
}