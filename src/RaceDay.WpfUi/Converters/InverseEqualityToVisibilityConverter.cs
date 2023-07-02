using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RaceDay.WpfUi.Converters;

/// <summary>
///     Converts a value to a visibility
///     If the value equals the parameter, then collapsed, otherwise visible
/// </summary>
public class InverseEqualityToVisibilityConverter : IValueConverter
{
    #region Interfaces Implement

    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && value.Equals(parameter))
            return Visibility.Collapsed;

        return Visibility.Visible;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;

    #endregion
}