using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace broCodeCalculator
{
    
    public class Calculator
    {
        private readonly static Regex _pattern = new(@"^[\d\.]+\s*[\-+*/]\s*[\d\.]+(?:\s*[\-+*/]\s*[\d\.]+)*$", RegexOptions.Compiled);
        private readonly static Regex _operators = new(@"[+/*\-]");

        //underscore class level fields.
        public static void QueryUserIntro()
        {
            Console.WriteLine("welcome to calculator");
            Console.WriteLine("Please enter your query:");
        }

        public static double Evaluate(string input)
        {
            Match match = _pattern.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException($"'{input}' is not properly formated.");
            }
           /* if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"'{input}' is not properly formated.");
            }*/


            input = ProcessOperation(input, "/");
            input = ProcessOperation(input, "*");
            input = ProcessOperation(input, "-");
            input = ProcessOperation(input, "+");

            return double.Parse(input);
        }
        static string ProcessOperation(string expression, string op)
        {

            Regex formulaPattern = new($@"([\d\.]+)\s*{Regex.Escape(op)}\s*([\d\.]+)");
            if (!_operators.IsMatch(op))
            {
                throw new ArgumentException($"'{op}' is not recognized.");
            }



            Match match;
            while((match = formulaPattern.Match(expression)).Success) 
            {
                bool num1Converted = double.TryParse(match.Groups[1].Value, out double num1);
                bool num2Converted = double.TryParse(match.Groups[2].Value, out double num2);

                if (!num1Converted)
                {
                    throw new ArgumentException($"'{match.Groups[1].Value}' is not a number.");

                }

                if (!num2Converted)
                {
                    throw new ArgumentException($"'{match.Groups[2].Value}' is not a number.");
                }


                double evaluatedNums = op switch
                {
                    "+" => num1 + num2,//could receive 2+2+2*3/6-1 -> 4+2*3/6-1 -> 6*3/6-1
                    "-" => num1 - num2,
                    "*" => num1 * num2,
                    "/" => num1 / num2,
                    _ => throw new ArgumentException("This can't happen"),//this would never happen because of regex
                };
                expression = expression.ReplaceFirst(match.Groups[0].Value, evaluatedNums.ToString());
            }
            return expression;
        }
    }
}
