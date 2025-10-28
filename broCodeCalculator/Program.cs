﻿using System;
using System.Text.RegularExpressions;

namespace broCodeCalculator
{
    public class MyClass
    {
        private const string EXIT = "EXIT";
#pragma warning disable SYSLIB1045, IDE0079
        private readonly static Regex _pattern = new(@"^([\d\.]+)\s*([-+*/])\s*([\d\.]+)$", RegexOptions.Compiled);
#pragma warning restore SYSLIB1045, IDE0079

        static void Main()//didn't need arg
        {
            //declare variables. inefficient. may not actually use. 
            // double num1;
            //double num2;
            //string opratr;
            //bool calcAgain = true;
            //string input;
            //Match match;
            //we want to continue calculating until we are told to exit.

            //lets try to mess with REGEX and parsing the string. 
            //string pattern = @"^([\d\.]+)\s*([-+*/])\s*([\d\.]+)$"; //I did a bad and copied this regex from the internet. (?:) == dosn't capture group. 
            //regex need anchors. 

            while (true)//Loop exiting is handled in conditions. I KNOW WHAT I DID. 
            {
                //ask for input
                //TODO, make this more robust. IE ask for a single line, parse the information. also check for good data. 
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
            //variables string TODO. 
            bool num1Converted = double.TryParse(match.Groups[1].Value, out double num1);
            bool num2Converted = double.TryParse(match.Groups[3].Value, out double num2);
            if (!num1Converted)//"failing faster sorta (not technically)
            {
                throw new ArgumentException($"'{match.Groups[1].Value}' is not a number.");
            }
            if (!num2Converted)//"failing faster sorta (not technically)
            {
                throw new ArgumentException($"'{match.Groups[3].Value}' is not a number.");
            }
            //double num1 = Convert.ToDouble(match.Groups[1].Value);
            string opratr = match.Groups[2].Value;
            //double num2 = Convert.ToDouble(match.Groups[3].Value);

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