using System;
using System.Windows.Input;

namespace RaceDay.WpfUi.Infrastructure;

public class RelayCommand : ICommand
{
    #region Fields

    private readonly Func<object?, bool> _canExecute;
    private readonly Action<object?> _execute;

    #endregion

    #region Constructors

    public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    #endregion

    #region Interfaces Implement

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => _canExecute(parameter);

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    #endregion
}