using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RaceDay.WpfUi.Infrastructure;

/// <summary>
///     Base class for observable objects with validation <br />
///     Implements <see cref="INotifyPropertyChanged" /> <br />
///     Implements <see cref="INotifyDataErrorInfo" />
/// </summary>
public abstract class ObservableObjectWithValidation : ObservableObject, INotifyDataErrorInfo
{
    #region Fields

    /// <summary>
    ///     A dictionary of errors, keyed by property name
    /// </summary>
    private readonly IDictionary<string, List<string?>> _errors = new Dictionary<string, List<string?>>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public bool HasErrors => _errors.Any() || ForceInitialErrorState;

    /// <summary>
    ///     This property is used to force the initial error state of the object, before any property has been set.
    ///     This is useful for when you want to show the error state of the object before the user has interacted with it.
    ///     For example, when a dialog is opened, you may want to force the error state to set the CanExecute state of a
    ///     command.
    /// </summary>
    public bool ForceInitialErrorState { get; set; }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return Array.Empty<string>();
        return _errors.TryGetValue(propertyName, out var error) ? error : Array.Empty<string>();
    }

    /// <inheritdoc />
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        bool result = base.SetField(ref field, value, propertyName);
        Validate(value, propertyName);
        return result;
    }

    #endregion

    /// <summary>
    ///     Validates the specified value against the validation attributes of the property
    /// </summary>
    /// <param name="val">The value to validate</param>
    /// <param name="propertyName">Name of the property.</param>
    private void Validate(object? val, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName))
            _errors.Remove(propertyName);

        ForceInitialErrorState = false;

        var context = new ValidationContext(this) { MemberName = propertyName };
        List<ValidationResult> results = new();

        if (!Validator.TryValidateProperty(val, context, results))
        {
            var errors = results.Select(x => x.ErrorMessage).ToList();
            if (errors.Any() && propertyName != null)
                _errors[propertyName] = errors;
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}