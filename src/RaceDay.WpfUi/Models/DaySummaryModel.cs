using System;

namespace RaceDay.WpfUi.Models;

public record DaySummaryModel
{
    #region Properties

    public int RaceDayId { get; init; }
    public string? RaceDayName { get; init; }
    public float SignUpFee { get; init; }
    public float LapDistanceKilometers { get; init; }
    public float LapDistanceMiles => LapDistanceKilometers * 0.621371f;
    public float PetrolCostPerLap { get; init; }
    public int TotalRaces { get; init; }
    public TimeSpan? RecordLapTime { get; init; }
    public string? RecordHolderName { get; init; }
    public float TotalIncome { get; init; }
    public float TotalCost { get; init; }
    public float TotalProfit => TotalIncome - TotalCost;
    public float AverageProfit => (TotalIncome - TotalCost) / TotalRaces;

    #endregion
}