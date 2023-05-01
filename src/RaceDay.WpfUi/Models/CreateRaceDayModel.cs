using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

public class CreateRaceDayModel : ObservableObjectWithValidation
{
    private string? _name = string.Empty;
    private float? _signUpFee;
    private float? _lapDistance;
    private float? _petrolCostPerLap;
    public Guid Guid { get; set; }

    [Required(ErrorMessage = "Name cannot be empty")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    [Required(ErrorMessage = "Sign up fee cannot be empty")]
    [Range(0, 1000, ErrorMessage = "Sign up fee must be between 0 and 1000")]
    public float? SignUpFee
    {
        get => _signUpFee;
        set => SetField(ref _signUpFee, value);
    }

    [Required(ErrorMessage = "Lap distance cannot be empty")]
    [Range(0, 1000, ErrorMessage = "Lap distance must be between 0 and 1000")]
    public float? LapDistance
    {
        get => _lapDistance;
        set => SetField(ref _lapDistance, value);
    }

    [Required(ErrorMessage = "Petrol cost per lap cannot be empty")]
    [Range(0, 1000, ErrorMessage = "Petrol cost per lap must be between 0 and 1000")]
    public float? PetrolCostPerLap
    {
        get => _petrolCostPerLap;
        set => SetField(ref _petrolCostPerLap, value);
    }
}