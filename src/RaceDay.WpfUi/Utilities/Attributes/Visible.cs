using System;

namespace RaceDay.WpfUi.Utilities.Attributes;

public class Visible : Attribute
{
    public bool IsVisible { get; }

    public Visible(bool isVisible)
    {
        IsVisible = isVisible;
    }
}