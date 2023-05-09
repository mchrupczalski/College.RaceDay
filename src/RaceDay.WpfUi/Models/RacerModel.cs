using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

public class RacerModel : ObservableObjectWithValidation
{
    #region Fields

    private byte? _age;
    private string? _racerName;
    private bool _isSelected;

    #endregion

    #region Properties

    public bool IsSelected
    {
        get => _isSelected;
        set => SetField(ref _isSelected, value);
    }

    [ReadOnly(true)]
    public int RacerId { get; init; }

    [Required(ErrorMessage = "Name cannot be empty")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
    public string? RacerName
    {
        get => _racerName;
        set
        {
            if (SetField(ref _racerName, value)) OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    [Required(ErrorMessage = "Age cannot be empty")]
    [Range(16, 100, ErrorMessage = "Age must be between 16 and 100")]
    public byte? Age
    {
        get => _age;
        set
        {
            if (SetField(ref _age, value)) OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    public bool HasAllRequiredData => !string.IsNullOrEmpty(RacerName) && Age.HasValue;

    #endregion
}