using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RaceDay.WpfUi.Converters;

public class InverseEqualityToVisibilityConverter : IValueConverter
{
    #region Interfaces Implement

    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && value.Equals(parameter)) return Visibility.Collapsed;

        return Visibility.Visible;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    #endregion
}