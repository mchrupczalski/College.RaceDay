using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

/// <summary>
///     Model for Racer
/// </summary>
public class RacerModel : ObservableObjectWithValidation
{
    #region Fields

    private byte? _age;
    private bool _isSelected;
    private string? _racerName;

    #endregion

    #region Properties

    /// <summary>
    ///     Indicates if the Racer is selected in the UI
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set => SetField(ref _isSelected, value);
    }

    /// <summary>
    ///     The unique identifier of the Racer
    /// </summary>
    [ReadOnly(true)]
    public int RacerId { get; init; }

    /// <summary>
    ///     The name of the Racer
    /// </summary>
    [Required(ErrorMessage = "Name cannot be empty")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    public string? RacerName
    {
        get => _racerName;
        set
        {
            if (SetField(ref _racerName, value))
                OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    /// <summary>
    ///     The age of the Racer
    /// </summary>
    [Required(ErrorMessage = "Age cannot be empty")]
    [Range(16, 100, ErrorMessage = "Age must be between 16 and 100")]
    public byte? Age
    {
        get => _age;
        set
        {
            if (SetField(ref _age, value))
                OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    /// <summary>
    ///     Indicates if the Racer is valid
    /// </summary>
    public bool HasAllRequiredData => !string.IsNullOrEmpty(RacerName) && Age.HasValue;

    #endregion
}