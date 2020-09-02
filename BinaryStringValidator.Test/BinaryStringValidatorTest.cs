using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinaryStringValidator.Test
{
    public class BinaryStringValidatorTest
    {
        [Fact]
        public void ValidateEmptyString_ReturnFalse()
        {

            BinaryStringValidator validator = new BinaryStringValidator();
            string input = "";

            bool result = validator.IsStringValid(input);

            Assert.False(result);
        }

        [Fact]
        public void ValidateStringWithDifferentNumbers_ReturnFalse()
        {

            BinaryStringValidator validator = new BinaryStringValidator();
            string input = "123";

            bool result = validator.IsStringValid(input);

            Assert.False(result);
        }

        [Theory]
        [InlineData("01011", false)]
        [InlineData("010110", false)]
        [InlineData("110010", true)]
        [InlineData("1100101100", true)]
        public void ValidateGoodStrings_ReturnTrueOrFalse(string input, bool expected)
        {

            BinaryStringValidator validator = new BinaryStringValidator();


            bool result = validator.IsStringValid(input);

            Assert.Equal(expected, result);
        }
    }
}
