using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RaceDay.WpfUi.Converters;

/// <summary>
///     Convenience converter to get the row index of an item in a list
/// </summary>
public class RowIndexConverter : IValueConverter
{
    #region Interfaces Implement

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        var item = (DependencyObject)value;
        var ic = ItemsControl.ItemsControlFromItemContainer(item);
        int offset = parameter == null ? 0 : System.Convert.ToInt32(parameter);

        return ic.ItemContainerGenerator.IndexFromContainer(item) + offset;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        Binding.DoNothing;

    #endregion
}