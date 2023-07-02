using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaceDay.WpfUi.Infrastructure;

/// <summary>
///     Base class for observable objects
///     Implements <see cref="INotifyPropertyChanged" />
/// </summary>
public abstract class ObservableObject : INotifyPropertyChanged
{
    #region Interfaces Implement

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    /// <summary>
    ///     Raises the <see cref="PropertyChanged" /> event.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Sets the field and raises the <see cref="PropertyChanged" /> event if the value has changed.
    /// </summary>
    /// <param name="field">A reference to the field to set.</param>
    /// <param name="value">The value to set the field to.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <returns>True if the value has changed, false otherwise.</returns>
    protected virtual bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}