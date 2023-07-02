using System;
using System.Globalization;
using System.Windows.Controls;

namespace RaceDay.WpfUi.ValidationRules;

/// <summary>
///     A validation rule for validating float values
/// </summary>
public class FloatValidationRule : ValidationRule
{
    #region Properties

    /// <summary>
    ///     The minimum value allowed
    /// </summary>
    public float Min { get; set; } = 0;

    /// <summary>
    ///     The maximum value allowed
    /// </summary>
    public float? Max { get; set; } = null;

    /// <summary>
    ///     The name of the property being validated
    /// </summary>
    public string PropertyName { get; set; } = "Value";

    #endregion

    #region Overrides

    /// <inheritdoc />
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        try
        {
            if (value is not string stringValue)
                return new ValidationResult(false, $"{PropertyName} is not a string");
            if (string.IsNullOrWhiteSpace(stringValue))
                return new ValidationResult(false, $"{PropertyName} is null or whitespace");
            if (!float.TryParse(stringValue, out float floatValue))
                return new ValidationResult(false, $"{PropertyName} is not a float");
            if (floatValue < Min)
                return new ValidationResult(false, $"{PropertyName} is less than {Min}");
            if (floatValue > Max)
                return new ValidationResult(false, $"{PropertyName} is greater than {Max.Value}");

            return ValidationResult.ValidResult;
        }
        catch (Exception e)
        {
            return new ValidationResult(false, $"Illegal characters or {e.Message}");
        }
    }

    #endregion
}