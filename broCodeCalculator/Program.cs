using System;
using System.Text.RegularExpressions;

namespace broCodeCalculator
{
    public class MyClass
    {
        static void Main(string[] args)
        {
            //declare variables
            double num1;
            double num2;
            string opratr;
            bool calcAgain = true;
            string input;
            Match match;
            //we want to continue calculating until we are told to exit.

            //lets try to mess with REGEX and parsing the string. 
            string pattern = @"(\d+)\s*([-+*/])\s*(\d+)"; //I did a bad and copied this regex from the internet. 
            while (calcAgain)
            {
                //ask for input
                //TODO, make this more robust. IE ask for a single line, parse the information. also check for good data. 
                Console.WriteLine("welcome to calculator");
                input = Console.ReadLine();
                match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    num1 = Convert.ToDouble(match.Groups[1].Value);
                    opratr = match.Groups[2].Value;
                    num2 = Convert.ToDouble(match.Groups[3].Value);

                    switch (opratr)
                    {
                        case "+":
                            Console.WriteLine(num1 + num2);
                            break;
                        case "-":
                            Console.WriteLine(num1 - num2);
                            break;
                        case "*":
                            Console.WriteLine(num1 * num2);
                            break;
                        case "/":
                            Console.WriteLine(num1 / num2);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("String format not recognized");
                }

            /*    Console.Write("please enter your first number: ");
                num1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("First number is: " + num1);
                Console.WriteLine("Please enter you second number: ");
                num2 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Second number is: " + num2);
            */

                //ask for the operator
               /* Console.WriteLine("Please enter an operator (+,-,*, or /");
                opratr = Console.ReadLine();*/

                //do the maths
          /*      switch(opratr)
                {
                    case "+":
                        Console.WriteLine(num1 + num2);
                        break;
                    case "-":
                        Console.WriteLine(num1 - num2);
                        break;
                    case "*":
                        Console.WriteLine(num1 * num2);
                        break;
                    case "/":
                        Console.WriteLine(num1 / num2);
                        break;
                }*/
                //I considered having some code to end the program but figured that like the calculator in most apps, it should 
                //stay open until the user just clicks the x. So there's no real reason to do that.
            }
        }
    }
}