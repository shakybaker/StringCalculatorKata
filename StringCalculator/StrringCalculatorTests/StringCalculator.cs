using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StrringCalculatorTests
{
    public class StringCalculator : IStringCalculator
    {
        public int Calculate(string input)
        {
            var delimiters = new string[] { ",", "\n" };
            string args;

            var match = Regex.Match(input, @"^//(?<delim>.*?)\n(?<args>.*$?)");
            if (match.Success)
            {
                var delims = match.Groups["delim"].Value.ToCharArray();
                var argMatch = Regex.Match(match.Groups["delim"].Value, @"\[.*\].*$");

                //TODO: sort out this filthy hack, need to get regex working here
                var d = string.Empty;
                foreach (var c in delims)
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

                args = match.Groups["args"].Value;
            }
            
            
            var numArray = input.Split(delimiters, StringSplitOptions.None);

            if (numArray.Length == 1)
                return ConvertToNumber(numArray[0]);

            var sum = 0;
            for (var i = 0; i < numArray.Length; i++)
                sum = sum + ConvertToNumber(numArray[i]);

            return sum;
        }

        private static int ConvertToNumber(string input)
        {
            int output;
            bool isNumber = int.TryParse(input, out output);

            if (isNumber)//TODO: should throw argument for non-numeric also
            {
                if (output < 0)
                    throw new ArgumentException();
                if (output > 1000)
                    output = 0;
            }

            return output;
        }
    }
}
