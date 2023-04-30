using System;

namespace RaceDay.WpfUi.Models;

public class RaceDayRaceModel
{
    public Guid Guid { get; set; }
    public Guid RaceDayGuid { get; set; }
    public DateTime RaceDate { get; set; }
    public int NumberOfRacers { get; set; }
    public int TotalNumberOfLaps { get; set; }
    public TimeSpan BestLapTime { get; set; }
    public string? BestLapTimeHolder { get; set; }
    public float TotalIncome { get; set; }
    public float TotalExpenses { get; set; }
    public float TotalProfit { get; set; }
}