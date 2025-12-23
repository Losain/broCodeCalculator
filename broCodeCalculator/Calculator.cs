using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace broCodeCalculator
{

    public static class Calculator
    {
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
        private readonly static Regex _pattern = new(@"^\-?[\d\.]+[+*/]\-?[\d\.]+(?:[+*/]\-?[\d\.]+)*$", RegexOptions.Compiled);
        private readonly static Regex _operators = new(@"[+/*\-]");
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.


        //underscore class level fields.
        public static void QueryUserIntro()
        {
            Console.WriteLine("welcome to calculator");
            Console.WriteLine("Please enter your query:");
        }

        public static double Evaluate(string copyOfVal)
        {
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
            copyOfVal = Regex.Replace(copyOfVal, @"\s", "");
            copyOfVal = Regex.Replace(copyOfVal, "\\-{2}", "+");
            copyOfVal = Regex.Replace(copyOfVal, "\\-", "+-");
            copyOfVal = Regex.Replace(copyOfVal, @"([/*-])\+", "$1");
            copyOfVal = Regex.Replace(copyOfVal, @"\+{2,}", "+");
            copyOfVal = Regex.Replace(copyOfVal, @"(^|\()[+/*]+", "$1");
            copyOfVal = Regex.Replace(copyOfVal, @"[+/*-]+($|\))", "$1");

            Regex parenthesesPattern = new(@"\(([^()]+)\)");
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
            Match parenMatch;
            while ((parenMatch = parenthesesPattern.Match(copyOfVal)).Success)
            {
                string innerExpression = parenMatch.Groups[1].Value;
                double innerResult = EvaluateExpression(innerExpression);
                copyOfVal = copyOfVal.ReplaceFirst(parenMatch.Groups[0].Value, innerResult.ToString());
            }

            static double EvaluateExpression(string expression)
            {
                if (double.TryParse(expression, out double singleValue))
                {
                    return singleValue;
                }

                Match match = _pattern.Match(expression);
                if (!match.Success)
                {
                    throw new ArgumentException($"'{expression}' is not properly formatted.");
                }

                expression = ProcessOperation(expression, "/");
                expression = ProcessOperation(expression, "*");
                expression = ProcessOperation(expression, "+");

                return double.Parse(expression);
            }

            static string ProcessOperation(string expression, string op)
            {

                Regex formulaPattern = new($@"(\-?[\d\.]+){Regex.Escape(op)}(\-?[\d\.]+)");
                if (!_operators.IsMatch(op))
                {
                    throw new ArgumentException($"'{op}' is not recognized.");
                }

                Match match;
                while ((match = formulaPattern.Match(expression)).Success)
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
                        "+" => num1 + num2,
                        "-" => num1 - num2,
                        "*" => num1 * num2,
                        "/" => num1 / num2,
                        _ => throw new ArgumentException("This can't happen"),
                    };
                    expression = expression.ReplaceFirst(match.Groups[0].Value, evaluatedNums.ToString());
                }
                return expression;
            }
            return EvaluateExpression(copyOfVal);
        }
    }
}
