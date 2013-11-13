using System;
using System.Text.RegularExpressions;

namespace StringCalculatorTests
{
    public class StringCalculator : IStringCalculator
    {
        public int Calculate(string input)
        {
            var delimiters = new string[] { ",", "\n" };
            var match = Regex.Match(input, @"^//(?<delim>.*?)\n(?<args>.*$?)");
            var args = input;
            if (match.Success)
            {
                var delims = match.Groups["delim"].Value.ToCharArray();
                args = match.Groups["args"].Value;
                var argMatch = Regex.Match(match.Groups["delim"].Value, @"\[.*\].*$");
                delimiters = AddNewDelimiters(delims, delimiters);
            }
            
            var numArray = args.Split(delimiters, StringSplitOptions.None);
            if (numArray.Length == 1)
                return ConvertToNumber(numArray[0]);
            var sum = 0;
            for (var i = 0; i < numArray.Length; i++)
                sum = sum + ConvertToNumber(numArray[i]);

            return sum;
        }

        private string[] AddNewDelimiters(char[] delims, string[] delimiters)
        {
            var d = string.Empty;
            foreach (var c in delims) //TODO: sort out this filthy hack, need to get regex working here
            {
                if (c != '[')
                {
                    if (c == ']')
                    {
                        Array.Resize(ref delimiters, delimiters.Length + 1);
                        delimiters[delimiters.Length - 1] = d;
                        d = string.Empty;
                    }
                    else
                        d += c;
                }
            }

            return delimiters;
        }

        private static int ConvertToNumber(string input)
        {
            int output;
            bool isNumber = int.TryParse(input, out output);

            if (isNumber)
            {
                if (output < 0)
                    throw new ArgumentException();
                if (output > 1000)
                    output = 0;
                return output;
            }
            else
                throw new ArgumentException();
        }
    }
}
