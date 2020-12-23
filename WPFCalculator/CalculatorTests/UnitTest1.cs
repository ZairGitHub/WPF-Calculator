using NUnit.Framework;
using CalculatorApp;

namespace CalculatorTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            var result = Calculator.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }
    }
}