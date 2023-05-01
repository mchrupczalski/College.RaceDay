using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RaceDay.WpfUi.Infrastructure;

public class ObservableObjectWithValidation : ObservableObject, INotifyDataErrorInfo
{
    #region Fields

    private readonly IDictionary<string, List<string?>> _errors = new Dictionary<string, List<string?>>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public bool HasErrors => _errors.Any();

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName)) return Array.Empty<string>();
        return _errors.TryGetValue(propertyName, out var error) ? error : Array.Empty<string>();
    }

    /// <inheritdoc />
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override bool SetField<T>(ref T field, T value,[CallerMemberName] string? propertyName = null)
    {
        bool result = base.SetField(ref field, value, propertyName);
        Validate(value, propertyName);
        return result;
    }

    #endregion

    private void Validate(object? val, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName)) _errors.Remove(propertyName);

        var context = new ValidationContext(this) { MemberName = propertyName };
        List<ValidationResult> results = new();

        if (!Validator.TryValidateProperty(val, context, results))
        {
            var errors = results.Select(x => x.ErrorMessage)
                                .ToList();
            if (errors.Any() && propertyName != null)
                _errors[propertyName] = errors;
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}