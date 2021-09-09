using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {

        [TestCase(17, "17")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(-6, "Fizz")]
        [TestCase(10, "Buss")]
        public void GetOutpuut_PassVariousNumbers_ReturnExpectedValues(int a, string expected)
        {
            var result = FizzBuzz.GetOutput(a);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
