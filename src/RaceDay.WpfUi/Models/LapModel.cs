using System;

namespace RaceDay.WpfUi.Models;

public class LapModel
{
    public Guid Guid { get; init; }
    public RaceDayModel? RaceDay { get; set; }
    public float LapDistanceKm { get; set; }
    public float PetrolCostPerLap { get; set; }
}