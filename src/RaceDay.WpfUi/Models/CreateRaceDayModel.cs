using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

public class CreateRaceDayModel : ObservableObject
{
    private string? _name = string.Empty;
    private float? _signUpFee;
    private float? _lapDistance;
    private float? _petrolCostPerLap;
    public Guid Guid { get; set; }

    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public float? SignUpFee
    {
        get => _signUpFee;
        set => SetField(ref _signUpFee, value);
    }

    public float? LapDistance
    {
        get => _lapDistance;
        set => SetField(ref _lapDistance, value);
    }

    public float? PetrolCostPerLap
    {
        get => _petrolCostPerLap;
        set => SetField(ref _petrolCostPerLap, value);
    }
}