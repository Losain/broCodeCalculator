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
        [InlineData("1/2*(1+2)", 1.5)]
        // Parentheses tests
        [InlineData("(5+3)", 8)]
        [InlineData("(2*3)+4", 10)]
        [InlineData("10-(2+3)", 5)]
        [InlineData("(10/2)*3", 15)]
        [InlineData("2*(3+4)", 14)]
        // Negative numbers
        [InlineData("-5+3", -2)]
        [InlineData("2*-3", -6)]
        [InlineData("-5*-2", 10)]
        [InlineData("10+-5", 5)]
        [InlineData("-10-5", -15)]
        // Double negatives
        [InlineData("1--2", 3)]
        [InlineData("5---3", 2)]
        // Chained operations (PEMDAS)
        [InlineData("2+3*4", 14)]
        [InlineData("10/2*3", 15)]
        [InlineData("2*3+4*5", 26)]
        // Decimal operations
        [InlineData("0.5+0.5", 1)]
        [InlineData("1.5*2", 3)]
        [InlineData("10/4", 2.5)]
        [InlineData("0.1+0.2", 0.3)]
        // Whitespace handling
        [InlineData("1 + 2", 3)]
        [InlineData("12 + 2", 14)]
        [InlineData("  5  *  2  ", 10)]
        // Complex expressions
        [InlineData("(2+3)*(4-1)", 15)]
        [InlineData("100/2/5", 10)]
        [InlineData("2+2+2+2", 8)]
        [InlineData("8", 8)]
        [InlineData("-5", -5)]
        [InlineData("3.14", 3.14)]
        // Test edge cases from your new regex preprocessing
        [InlineData("1+2-", 3)]  // Trailing operator cleanup
        [InlineData("(2+3-)", 5)]  // Operator cleanup in parentheses
        [InlineData("1++2", 3)]  // Multiple plus signs

        public void Test2(string input, double expected)
        {
            double actual = Calculator.Evaluate(input);
            //actual = Math.Round(actual, 5);
            Assert.Equal(expected, actual, 6);//could round before comparing??
        }

        [Fact]
        public void SadPath()
        {
            string input = "hihowareyeah";
            ArgumentException e = Assert.Throws<ArgumentException>(() => Calculator.Evaluate(input));//$"'{input}' is not properly formated."
            Assert.Equal($"'{input}' is not properly formatted.", e.Message);
        }

        [Theory]
        [InlineData("1.1.2+2", "'1.1.2' is not a number.")] //$"'{match.Groups[1].Value}' is not a number."
        [InlineData("2+2.2.3", "'2.2.3' is not a number.")]
        [InlineData("", "'' is not properly formatted.")]
        [InlineData(" ", "'' is not properly formatted.")]
        [InlineData("sillybillt+tillywilly", "'sillybillt+tillywilly' is not properly formatted.")]
        [InlineData("2*/3", "'2*/3' is not properly formatted.")]  // Invalid operator sequence (if applicable)


        public void NumberEval(string input, string expected)
        {
            ArgumentException e = Assert.Throws<ArgumentException>(() => Calculator.Evaluate(input));
            Assert.Equal(expected, e.Message);
        }

        //Fact for null arg exception. I"M 100% certain I did this wrong. 
        [Fact]
        public void NullArgExceptn()
        {
            string? input = null;//this is a test we are purposly handling null in the exceptions.
            ArgumentException e = Assert.Throws<ArgumentNullException>(() => Calculator.Evaluate(input));//$"'{input}' is not properly formated."
            Assert.Equal("Value cannot be null. (Parameter 'input')", e.Message);
        }
    }
}