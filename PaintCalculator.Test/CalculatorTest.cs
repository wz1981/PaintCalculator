using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaintCalculator.Test
{
    public class CalculatorTest
    {
        [Theory]       
        [InlineData(",2,3", "{\"Error\":\"error\",\"ErrorMessage\":\"Invalid data\"}")]
        public void Add_ReturnZeroValue_WhenPassingEmptyString(string calculation, string expected)
        {
            //Arrange
            var cal = new Calculator();
            //Act
            var result = cal.AreaAmountVoulmeInfo(calculation);
            //Assert
            result.Should().Be(expected);
        }
        [Theory]
        [InlineData(",2,3", "{\"Error\":\"error\",\"ErrorMessage\":\"Invalid data\"}")]
        [InlineData("1,2", "{\"Error\":\"error\",\"ErrorMessage\":\"Invalid data\"}")]
        [InlineData("1,,3", "{\"Error\":\"error\",\"ErrorMessage\":\"Invalid data\"}")]        
        public void Add_ReturnInvalidDataMessage_WhenAnyOfFieldsEmptyString(string calculation, string expected)
        {
            //Arrange
            var cal = new Calculator();
            //Act
            var result = cal.AreaAmountVoulmeInfo(calculation);
            //Assert
            result.Should().Be(expected);
        }
        [Theory]
        [InlineData("5,-6,4","-6")]
        [InlineData("-5.67,-6,-4","-5.67,-6,-4")]
        [InlineData("5,-6.89,-4","-6.89,-4")]
        public void Add_ReturnNegativeValueNotAllowedMessage_WhenAnyOfFieldsIsNegative(string calculation, string negativeNumbers)
        {
            //Arrange
            var cal = new Calculator();
            //Act
            Action action = ()=> cal.AreaAmountVoulmeInfo(calculation);
            //Assert
            action.Should()
                .Throw<Exception>()
                .WithMessage("Negatives not allowed:" + negativeNumbers);
        }

        [Theory]
        [InlineData("4.5,10,6", "{\"Area\":45.0,\"Amount\":174.0,\"Volume\":270.0}")]
        public void Add_ReturnValidJsonString_WhenPassValidNumbers(string calculation, string expected)
        {
            //Arrange
            var cal = new Calculator();
            //Act
            var result = cal.AreaAmountVoulmeInfo(calculation);
            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("4.567,10.321,6.12", "{\"Area\":47.14,\"Amount\":182.23,\"Volume\":288.47}")]
        public void Add_ReturnValidrRoundValueWithJsonString_WhenPassValidNumbersWithDecimals(string calculation, string expected)
        {
            //Arrange
            var cal = new Calculator();
            //Act
            var result = cal.AreaAmountVoulmeInfo(calculation);
            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("4.567|10.321|6.12", "{\"Area\":47.14,\"Amount\":182.23,\"Volume\":288.47}")]
        [InlineData("4.567?10.321?6.12", "{\"Area\":47.14,\"Amount\":182.23,\"Volume\":288.47}")]
        [InlineData("4.567/10.321/6.12", "{\"Area\":47.14,\"Amount\":182.23,\"Volume\":288.47}")]
        public void Add_ReturnValidrRoundValueWithJsonString_WhenUsingDifferentDelimiters(string calculation, string expected)
        {
            //Arrange
            var cal = new Calculator();
            //Act
            var result = cal.AreaAmountVoulmeInfo(calculation);
            //Assert
            result.Should().Be(expected);
        }
    }
    
}
