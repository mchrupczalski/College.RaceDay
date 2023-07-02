using System;
using System.Windows.Input;

namespace RaceDay.WpfUi.Infrastructure;

/// <summary>
///     A command that can be bound to a UI element
/// </summary>
public class RelayCommand : ICommand
{
    #region Fields

    private readonly Func<object?, bool> _canExecute;
    private readonly Action<object?> _execute;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
    /// </summary>
    /// <param name="execute">A delegate to execute when the command is executed</param>
    /// <param name="canExecute">A delegate to execute to determine if the command can be executed</param>
    public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    #endregion

    #region Interfaces Implement

    /// <summary>
    ///     Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    ///     Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">An optional parameter to pass to the command</param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object? parameter) => _canExecute(parameter);

    /// <summary>
    ///     Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object? parameter) => _execute(parameter);

    #endregion
}