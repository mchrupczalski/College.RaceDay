using System;

namespace RaceDay.WpfUi.Models;

public class RaceSummaryModel
{
    public int RaceId { get; init; }
    public int RaceDayId { get; init; }
    public DateTime? RaceDate { get; init; }
    public int TotalRacers { get; init; }
    public int TotalLaps { get; init; }
    public TimeSpan BestLapTime { get; init; }
    public string? BestLapTimeHolder { get; init; }
    public float TotalIncome { get; init; }
    public float TotalExpenses { get; init; }
    public float TotalProfit { get; init; }
}