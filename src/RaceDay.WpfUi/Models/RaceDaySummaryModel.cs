using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

public record RaceDaySummaryModel
{
    #region Properties

    public Guid Guid { get; init; }
    public string? Name { get; init; }
    public float SignUpFee { get; init; }
    public float LapDistanceKilometers { get; init; }
    public float LapDistanceMiles => LapDistanceKilometers * 0.621371f;
    public float PetrolCostPerLap { get; init; }
    public int TotalRaces { get; init; }
    public TimeSpan? RecordLap { get; init; }
    public string? RecordHolderName { get; init; }
    public float AverageProfit { get; init; }

    #endregion
}