using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RaceDay.WpfUi.Controls;

/// <summary>
///     Interaction logic for CardDataField.xaml
///     A field for displaying data in a card
/// </summary>
public class CardDataField : Control
{
    #region Static Fields and Const

    /// <summary>
    ///     The header dependency property
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header), typeof(string), typeof(CardDataField), new PropertyMetadata(default(string)));

    /// <summary>
    ///     The image source dependency property
    /// </summary>
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
        nameof(ImageSource), typeof(ImageSource), typeof(CardDataField), new PropertyMetadata(default(ImageSource)));

    /// <summary>
    ///     The value dependency property
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(string), typeof(CardDataField), new PropertyMetadata(default(string)));

    #endregion

    #region Properties

    /// <summary>
    ///     Gets or sets the header.
    /// </summary>
    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    ///     Gets or sets the value.
    /// </summary>
    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    ///     Gets or sets the image source.
    /// </summary>
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes static members of the <see cref="CardDataField" /> class.
    /// </summary>
    static CardDataField()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CardDataField), new FrameworkPropertyMetadata(typeof(CardDataField)));
    }

    #endregion
}