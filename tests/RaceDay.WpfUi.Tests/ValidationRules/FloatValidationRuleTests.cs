using System.Globalization;
using RaceDay.WpfUi.ValidationRules;

namespace RaceDay.WpfUi.Tests.ValidationRules;

public class FloatValidationRuleTests
{
    
    // Test Validate when string is null or whitespace returns false
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_ReturnsFalse_WhenStringIsNullOrWhitespace(object value)
    {
        var rule = new FloatValidationRule();
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.False(result.IsValid);
    }
    
    // Test Validate when string is not a float returns false
    [Theory]
    [InlineData("abc")]
    [InlineData("1.2.3")]
    public void Validate_ReturnsFalse_WhenStringIsNotAFloat(object value)
    {
        var rule = new FloatValidationRule();
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.False(result.IsValid);
    }
    
    // Test Validate when string is less than Min returns false
    [Theory]
    [InlineData("0.0")]
    [InlineData("-1.0")]
    public void Validate_ReturnsFalse_WhenStringIsLessThanMin(object value)
    {
        var rule = new FloatValidationRule { Min = 1 };
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.False(result.IsValid);
    }
    
    // Test Validate when string is greater than Max returns false
    [Theory]
    [InlineData("2.1")]
    [InlineData("3.0")]
    public void Validate_ReturnsFalse_WhenStringIsGreaterThanMax(object value)
    {
        var rule = new FloatValidationRule { Max = 2 };
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.False(result.IsValid);
    }
    
    // Test Validate when string is a float and between Min and Max returns true
    [Theory]
    [InlineData("1.0")]
    [InlineData("1.5")]
    [InlineData("2.0")]
    public void Validate_ReturnsTrue_WhenStringIsAFloatAndBetweenMinAndMax(object value)
    {
        var rule = new FloatValidationRule { Min = 1, Max = 2 };
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.True(result.IsValid);
    }
}