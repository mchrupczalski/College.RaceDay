using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RaceDay.WpfUi.Controls;

public class CardDataField : Control
{
    #region Static Fields and Const

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header), typeof(string), typeof(CardDataField), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
        nameof(ImageSource), typeof(ImageSource), typeof(CardDataField), new PropertyMetadata(default(ImageSource)));

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(string), typeof(CardDataField), new PropertyMetadata(default(string)));

    #endregion

    #region Properties

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    #endregion

    #region Constructors

    static CardDataField()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CardDataField), new FrameworkPropertyMetadata(typeof(CardDataField)));
    }

    #endregion
}