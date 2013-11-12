using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrringCalculatorTests;

namespace StringCalculatorTests
{
    [TestFixture]
    public class UnitTests
    {

        [Test]
        public void An_empty_string_returns_zero()
        {
            var calc = new StringCalculator();
            var actual = calc.Calculate(string.Empty);

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void A_single_number_returns_the_value()
        {
            var calc = new StringCalculator();
            var actual = calc.Calculate("1");

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void Two_numbers_comma_delimited_returns_the_sum()
        {
            var calc = new StringCalculator();
            var actual = calc.Calculate("1,2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Two_numbers_newline_delimited_returns_the_sum()
        {
            var calc = new StringCalculator();
            var actual = calc.Calculate("1\n2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Three_numbers_delimited_either_way_returns_the_sum()
        {
            var calc = new StringCalculator();
            var actual = calc.Calculate("1\n2,3");

            Assert.AreEqual(6, actual);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Negative_numbers_throw_an_exception()
        {
            var calc = new StringCalculator();
            var actual = calc.Calculate("-1");
        }

        [Test]
        public void Numbers_greater_than_1000_are_ignored()
        {
            var calc = new StringCalculator();
            var actual = calc.Calculate("1,1001");

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void A_single_char_delimiter_can_be_defined_on_the_first_line()
        {
            //NOTE: my assumption here is that there will be a '\n' after the delimiter
            var calc = new StringCalculator();
            var actual = calc.Calculate("//[#][x][y][xxx]\n1#2");

            Assert.AreEqual(3, actual);
        }

        /*
        [Test]
        public void A_multi_char_delimiter_can_be_defined_on_the_first_line()
        {
        }

        [Test]
        public void Many_single_or_multichar_delimiters_can_be_defined()
        {
        }
         */
    }
}
