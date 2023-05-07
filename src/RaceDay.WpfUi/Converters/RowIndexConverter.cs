using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RaceDay.WpfUi.Converters;

public class RowIndexConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, 
                          System.Globalization.CultureInfo culture)
    {
        var item = (DependencyObject)value;
        var ic = ItemsControl.ItemsControlFromItemContainer(item);
        int offset = parameter == null ? 0 : System.Convert.ToInt32(parameter);

        return ic.ItemContainerGenerator.IndexFromContainer(item) + offset;
    }

    public object ConvertBack(object value, Type targetType, object parameter, 
                              System.Globalization.CultureInfo culture) =>
        Binding.DoNothing;
}