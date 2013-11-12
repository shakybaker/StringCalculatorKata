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

            var delimMatch = Regex.Match(input, @"^//(?<delim>.*?)\n(?<args>.*$?)");
            if (delimMatch.Success)
            {
                string delims = delimMatch.Groups["delim"].Value;
                var argMatch = Regex.Match(delims, @"\[.*\].*$");
                var i = 0;
                while (argMatch.Success)
                {
                    Array.Resize(ref delimiters, delimiters.Length + 1);
                    delimiters[delimiters.Length - 1] = argMatch.Groups[i].Value;
                    argMatch.NextMatch();
                    i++;
                }

                args = delimMatch.Groups["args"].Value;
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
