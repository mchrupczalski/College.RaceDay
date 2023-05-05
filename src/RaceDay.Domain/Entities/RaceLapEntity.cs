namespace RaceDay.Domain.Entities;

public record RaceLapEntity : EntityBase
{
    #region Properties

    public int RaceDayId { get; init; }
    public int RaceNumber { get; init; }
    public int RacerId { get; init; }
    public int LapNumber { get; init; }
    public double LapTimeSeconds { get; init; }

    #endregion
}