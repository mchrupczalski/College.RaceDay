using System.ComponentModel.DataAnnotations;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

/// <summary>
///     A model for creating new Race Days
/// </summary>
public class NewRaceDayModel : ObservableObjectWithValidation
{
    #region Fields

    private float? _lapDistance;
    private string? _name = string.Empty;
    private float? _petrolCostPerLap;
    private float? _signUpFee;

    #endregion

    #region Properties

    /// <summary>
    ///     The name of the race day
    /// </summary>
    [Required(ErrorMessage = "Name cannot be empty")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string? Name
    {
        get => _name;
        set
        {
            if (SetField(ref _name, value))
                OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    /// <summary>
    ///     The sign up fee
    /// </summary>
    [Required(ErrorMessage = "Sign up fee cannot be empty")]
    [Range(0, 1000, ErrorMessage = "Sign up fee must be between 0 and 1000")]
    public float? SignUpFee
    {
        get => _signUpFee;
        set
        {
            if (SetField(ref _signUpFee, value))
                OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    /// <summary>
    ///     The Lap distance in kilometers
    /// </summary>
    [Required(ErrorMessage = "Lap distance cannot be empty")]
    [Range(0, 1000, ErrorMessage = "Lap distance must be between 0 and 1000")]
    public float? LapDistance
    {
        get => _lapDistance;
        set
        {
            if (SetField(ref _lapDistance, value))
                OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    /// <summary>
    ///     The estimated petrol cost per lap
    /// </summary>
    [Required(ErrorMessage = "Petrol cost per lap cannot be empty")]
    [Range(0, 1000, ErrorMessage = "Petrol cost per lap must be between 0 and 1000")]
    public float? PetrolCostPerLap
    {
        get => _petrolCostPerLap;
        set
        {
            if (SetField(ref _petrolCostPerLap, value))
                OnPropertyChanged(nameof(HasAllRequiredData));
        }
    }

    /// <summary>
    ///     Indicates whether the model has all the required data
    /// </summary>
    public bool HasAllRequiredData => !string.IsNullOrEmpty(Name) && SignUpFee.HasValue && LapDistance.HasValue && PetrolCostPerLap.HasValue;

    #endregion
}