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
            double actual = broCodeCalculator.MyClass.Evaluate(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SadPath()
        {
            string input = "hihowareyeah";
            ArgumentException e = Assert.Throws<ArgumentException>(() => broCodeCalculator.MyClass.Evaluate(input));//$"'{input}' is not properly formated."
            Assert.Equal($"'{input}' is not properly formated.", e.Message);
        }

        [Theory]
        [InlineData("1.1.2+2", "'1.1.2' is not a number.")] //$"'{match.Groups[1].Value}' is not a number."
        [InlineData("2+2.2.3", "'2.2.3' is not a number.")]
        [InlineData("", "'' is not properly formated.")]
        //[InlineData(null, "'' is not properly formated.")] null is not captured. 

        //can't/don't overload unit toast. 
        public void NumberEval(string input, string expected)
        {
            ArgumentException e = Assert.Throws<ArgumentException>(() => broCodeCalculator.MyClass.Evaluate(input));
            Assert.Equal(expected, e.Message);
        }

        //Fact for null arg exception.
        //white space
        //random string 
    }
}