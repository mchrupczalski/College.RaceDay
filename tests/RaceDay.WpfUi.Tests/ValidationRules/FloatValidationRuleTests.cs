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
    [InlineData(1f, "0.0")]
    [InlineData(1f, "-1.0")]
    public void Validate_ReturnsFalse_WhenStringIsLessThanMin(float min, object value)
    {
        var rule = new FloatValidationRule { Min = min };
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.False(result.IsValid);
    }
    
    // Test Validate when string is greater than Max returns false
    [Theory]
    [InlineData(2f,"2.1")]
    [InlineData(2f, "3.0")]
    public void Validate_ReturnsFalse_WhenStringIsGreaterThanMax(float max, object value)
    {
        var rule = new FloatValidationRule { Max = max };
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.False(result.IsValid);
    }
    
    // Test Validate when string is a float and between Min and Max returns true
    [Theory]
    [InlineData(1f, 2f, "1.0")]
    [InlineData(1f, 2f, "1.5")]
    [InlineData(1f, 2f, "2.0")]
    public void Validate_ReturnsTrue_WhenStringIsAFloatAndBetweenMinAndMax(float min, float max, object value)
    {
        var rule = new FloatValidationRule { Min = min, Max = max };
        var result = rule.Validate(value, CultureInfo.CurrentCulture);
        Assert.True(result.IsValid);
    }
}