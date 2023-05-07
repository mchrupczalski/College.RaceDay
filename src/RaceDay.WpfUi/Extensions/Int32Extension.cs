using System;
using System.Windows.Markup;

namespace RaceDay.WpfUi.Extensions;

public sealed class Int32Extension : MarkupExtension
{
    #region Properties

    public int Value { get; set; }

    #endregion

    #region Constructors

    public Int32Extension(int value) => Value = value;

    #endregion

    #region Overrides

    public override object ProvideValue(IServiceProvider sp) => Value;

    #endregion
}