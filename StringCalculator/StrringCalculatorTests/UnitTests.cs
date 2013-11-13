using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculatorTests;

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
        [TestCase("1", 1)]
        public void A_single_number_returns_the_value(string input, int expected)
        {
            var actual = calc.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("1,2", 3)]
        public void Two_numbers_comma_delimited_returns_the_sum(string input, int expected)
        {
            var actual = calc.Calculate(input);

            Assert.AreEqual(3, actual);
        }

        [Test]
        [TestCase("1\n2", 3)]
        public void Two_numbers_newline_delimited_returns_the_sum(string input, int expected)
        {
            var actual = calc.Calculate(input);

            Assert.AreEqual(3, actual);
        }

        [Test]
        [TestCase("1\n2,3", 6)]
        public void Three_numbers_delimited_either_way_returns_the_sum(string input, int expected)
        {
            var actual = calc.Calculate(input);

            Assert.AreEqual(6, actual);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Negative_numbers_throw_an_exception()
        {
            var actual = calc.Calculate("-1");
        }

        [Test]
        [TestCase("1,1001", 1)]
        public void Numbers_greater_than_1000_are_ignored(string input, int expected)
        {
            var actual = calc.Calculate(input);

            Assert.AreEqual(1, actual);
        }

        [Test]
        [TestCase("//[#]\n1#2", 3)]
        public void A_single_char_delimiter_can_be_defined_on_the_first_line(string input, int expected)
        {
            //TODO: my assumption here is that there will be a '\n' after the delimiter - add a test to check
            var actual = calc.Calculate(input);

            Assert.AreEqual(3, actual);
        }

        [Test]
        [TestCase("//[##]\n1##2", 3)]
        public void A_multi_char_delimiter_can_be_defined_on_the_first_line(string input, int expected)
        {
            //TODO: my assumption here is that the "[]" chars aren't used as delimiters - add a test to check
            var actual = calc.Calculate(input);

            Assert.AreEqual(3, actual);
        }

        [Test]
        [TestCase("//[##][@][^^^^^]\n1##2@3^^^^^4", 10)]
        public void Many_single_or_multichar_delimiters_can_be_defined(string input, int expected)
        {
            var actual = calc.Calculate(input);

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
