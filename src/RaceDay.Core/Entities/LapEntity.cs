﻿namespace RaceDay.Core.Entities;

public class LapEntity
{
    public Guid Guid { get; init; }
    public Guid RaceDayGuid { get; init; }
    public float LapDistanceM { get; set; }
    public float LapDistanceKm { get; set; }
    public float PetrolCostPerLap { get; set; }
}