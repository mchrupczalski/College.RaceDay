namespace RaceDay.Domain.DTOs;

public record RaceDayRaceDto
{
    #region Properties

    public int RaceDayId { get; init; }
    public int RaceNumber { get; init; }
    public DateTime RaceDate { get; init; }
    public int NumberOfRacers { get; init; }
    public int TotalNumberOfLaps { get; init; }
    public TimeSpan BestLapTime { get; init; }
    public string? BestLapTimeHolder { get; init; }
    public float TotalIncome { get; init; }
    public float TotalExpenses { get; init; }
    public float TotalProfit { get; init; }

    #endregion
}