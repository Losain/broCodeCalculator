using System.IO.Compression;
using broCodeCalculator;

namespace xUnit_broCodeCalculator
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("12+2", 14)]
        [InlineData("12-2", 10)]
        [InlineData("12*2", 24)]
        [InlineData("12/2", 6)]

        public void Test2(string input, double expected)
        {
            Calculator calc = new Calculator();
            double actual = Calculator.Evaluate(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SadPath()
        {
            Calculator calculator = new();
            string input = "hihowareyeah";
            ArgumentException e = Assert.Throws<ArgumentException>(() => Calculator.Evaluate(input));//$"'{input}' is not properly formated."
            Assert.Equal($"'{input}' is not properly formated.", e.Message);
        }

        [Theory]
        [InlineData("1.1.2+2", "'1.1.2' is not a number.")] //$"'{match.Groups[1].Value}' is not a number."
        [InlineData("2+2.2.3", "'2.2.3' is not a number.")]
        [InlineData("", "'' is not properly formated.")]
        [InlineData(" ", "' ' is not properly formated.")]
        [InlineData("sillybillt+tillywilly", "'sillybillt+tillywilly' is not properly formated.")]

        public void NumberEval(string input, string expected)
        {
            Calculator calculator = new();
            ArgumentException e = Assert.Throws<ArgumentException>(() => Calculator.Evaluate(input));
            Assert.Equal(expected, e.Message);
        }

        //Fact for null arg exception. I"M 100% certain I did this wrong. 
        [Fact]
        public void NullArgExceptn()
        {
            Calculator calculator = new();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string input = null;//this is a test we are purposly handling null in the exceptions.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            ArgumentException e = Assert.Throws<ArgumentNullException>(() => Calculator.Evaluate(input));//$"'{input}' is not properly formated."
            Assert.Equal("Value cannot be null. (Parameter 'input')", e.Message);
        }
    }
}