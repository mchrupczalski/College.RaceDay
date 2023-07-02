using System;
using System.Windows.Markup;

namespace RaceDay.WpfUi.Extensions;

/// <summary>
///     Markup extension to allow for int32 values in xaml
/// </summary>
public sealed class Int32Extension : MarkupExtension
{
    #region Properties

    /// <summary>
    ///     Gets or sets the value.
    /// </summary>
    public int Value { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="Int32Extension" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Int32Extension(int value) => Value = value;

    #endregion

    #region Overrides

    /// <summary>
    ///     Returns an object that is provided as the value of the target property for this markup extension.
    /// </summary>
    /// <param name="sp">Object that can provide services for the markup extension.</param>
    public override object ProvideValue(IServiceProvider sp) => Value;

    #endregion
}