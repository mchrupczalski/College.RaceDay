using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RaceDay.WpfUi.Converters;

/// <summary>
///     Converts a value to a visibility
///     If the value equals the parameter, then visible, otherwise collapsed
/// </summary>
public class EqualityToVisibilityConverter : IValueConverter
{
    #region Interfaces Implement

    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && value.Equals(parameter))
            return Visibility.Visible;

        return Visibility.Collapsed;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    #endregion
}