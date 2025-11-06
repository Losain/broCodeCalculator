using System;
using System.Text.RegularExpressions;

namespace broCodeCalculator
{
    public class MyClass
    {
        private const string EXIT = "EXIT";
#pragma warning disable SYSLIB1045, IDE0079
        private readonly static Regex _pattern = new(@"^([\d\.]+)\s*([-+*/])\s*([\d\.]+)$", RegexOptions.Compiled);
#pragma warning restore SYSLIB1045, IDE0079

        static void Main()
        {

            while (true)//Loop exiting is handled in conditions. I KNOW WHAT I DID. 
            {
                Calculator calculator = new Calculator();

                calculator.QueryUserIntro();

                string input = Console.ReadLine();
                input = input.Trim();

                if (input.Equals(EXIT, StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("BYE");
                    break;
                }
                try
                {
                    Console.WriteLine(calculator.Evaluate(input));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}