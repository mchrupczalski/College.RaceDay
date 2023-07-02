using System;
using System.Globalization;
using System.Windows.Data;

namespace RaceDay.WpfUi.Converters;

/// <summary>
///     Converts a boolean to its inverse
/// </summary>
public class InverseBooleanConverter : IValueConverter
{
    #region Interfaces Implement

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return !b;

        return value;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return !b;

        return value;
    }

    #endregion
}