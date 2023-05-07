namespace RaceDay.Domain.DTOs;

public record RaceSummaryDto
{
    #region Properties

    public int RaceId { get; init; }
    public int RaceDayId { get; init; }
    public DateTime RaceDate { get; init; }
    public int TotalRacers { get; init; }
    public int TotalLaps { get; init; }
    public TimeSpan BestLapTime { get; init; }
    public string? BestLapTimeHolder { get; init; }
    public float TotalIncome { get; init; }
    public float TotalExpense { get; init; }

    #endregion
}