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
                Console.WriteLine("welcome to calculator");
                string input = Console.ReadLine();
                input = input.Trim();

                if (input.Equals(EXIT, StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("BYE");
                    break;
                }
                try
                {
                    Console.WriteLine(Evaluate(input));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static double Evaluate(string input)
        {
            Match match = _pattern.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException($"'{input}' is not properly formated.");
            }

            bool num1Converted = double.TryParse(match.Groups[1].Value, out double num1);
            bool num2Converted = double.TryParse(match.Groups[3].Value, out double num2);

            if (!num1Converted)
            {
                throw new ArgumentException($"'{match.Groups[1].Value}' is not a number.");

            }

            if (!num2Converted)
            {
                throw new ArgumentException($"'{match.Groups[3].Value}' is not a number.");
            }

            string opratr = match.Groups[2].Value;

            return opratr switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num1 / num2,
                _ => throw new ArgumentException("This can't happen"),//this would never happen because of regex
            };
        }
    }
}