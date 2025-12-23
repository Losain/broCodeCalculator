using System;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("xUnit_broCodeCalculator.UnitTest1.cs")]


namespace broCodeCalculator
{
    public class MyClass
    {
        private const string EXIT = "EXIT";
        static void Main()
        {

            while (true)//Loop exiting is handled in conditions. I KNOW WHAT I DID. 
            {

                Calculator.QueryUserIntro();

                string input = Console.ReadLine();
                input = input.Trim();

                if (input.Equals(EXIT, StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("BYE");
                    break;
                }
                try
                {
                    Console.WriteLine(Calculator.Evaluate(input));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}