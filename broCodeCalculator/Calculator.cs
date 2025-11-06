using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace broCodeCalculator
{
    internal class Calculator
    {
        private readonly static Regex _pattern = new(@"^([\d\.]+)\s*([-+*/])\s*([\d\.]+)$", RegexOptions.Compiled);
        public void QueryUserIntro()
        {
            Console.WriteLine("welcome to calculator");
            Console.WriteLine("Please enter your query:");
        }

        public double Evaluate(string input)
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
