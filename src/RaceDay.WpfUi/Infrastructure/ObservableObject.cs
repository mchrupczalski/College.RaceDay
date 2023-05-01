using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaceDay.WpfUi.Infrastructure;

public abstract class ObservableObject : INotifyPropertyChanged
{
    #region Interfaces Implement

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}