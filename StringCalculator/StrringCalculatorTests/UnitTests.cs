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
        IStringCalculator calc;

        [SetUp]
	    public void RunBeforeAnyTests()
	    {
            calc = new StringCalculator();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void An_empty_string_throws_an_exception()
        {
            var actual = calc.Calculate("");
        }

        [Test]
        public void A_single_number_returns_the_value()
        {
            var actual = calc.Calculate("1");

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void Two_numbers_comma_delimited_returns_the_sum()
        {
            var actual = calc.Calculate("1,2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Two_numbers_newline_delimited_returns_the_sum()
        {
            var actual = calc.Calculate("1\n2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Three_numbers_delimited_either_way_returns_the_sum()
        {
            var actual = calc.Calculate("1\n2,3");

            Assert.AreEqual(6, actual);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Negative_numbers_throw_an_exception()
        {
            var actual = calc.Calculate("-1");
        }

        [Test]
        public void Numbers_greater_than_1000_are_ignored()
        {
            var actual = calc.Calculate("1,1001");

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void A_single_char_delimiter_can_be_defined_on_the_first_line()
        {
            //TODO: my assumption here is that there will be a '\n' after the delimiter - add a test to check
            var actual = calc.Calculate("//[#]\n1#2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void A_multi_char_delimiter_can_be_defined_on_the_first_line()
        {
            //TODO: my assumption here is that the "[]" chars aren't used as delimiters - add a test to check
            var actual = calc.Calculate("//[##]\n1##2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Many_single_or_multichar_delimiters_can_be_defined()
        {
            var actual = calc.Calculate("//[##][@][^^^^^]\n1##2@3^^^^^4");

            Assert.AreEqual(10, actual);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Sending_non_numeric_chars_as_an_argument_throws_exception()
        {
            var actual = calc.Calculate("x");
        }
    }
}
