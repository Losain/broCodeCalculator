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
    }
}