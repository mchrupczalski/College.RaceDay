using System;
using System.Globalization;
using System.Windows.Controls;

namespace RaceDay.WpfUi.ValidationRules;

public class FloatValidationRule : ValidationRule
{
    public float Min { get; set; } = 0;
    public float? Max { get; set; } = null;

    public string PropertyName { get; set; } = "Value";
    
    #region Overrides of ValidationRule

    /// <inheritdoc />
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        try
        {
            if (value is not string stringValue) return new ValidationResult(false,                    $"{PropertyName} is not a string");
            if (string.IsNullOrWhiteSpace(stringValue)) return new ValidationResult(false,             $"{PropertyName} is null or whitespace");
            if (!float.TryParse(stringValue, out float floatValue)) return new ValidationResult(false, $"{PropertyName} is not a float");
            if (floatValue < Min) return new ValidationResult(false,                                   $"{PropertyName} is less than {Min}");
            if (Max.HasValue && floatValue > Max.Value) return new ValidationResult(false,             $"{PropertyName} is greater than {Max.Value}");
            
            return ValidationResult.ValidResult;
        }
        catch (Exception e)
        {
            return new ValidationResult(false, $"Illegal characters or {e.Message}");
        }
    }

    #endregion
}