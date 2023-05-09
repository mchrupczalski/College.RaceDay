namespace RaceDay.Domain.DTOs;

public record RaceLapDto
{
    #region Properties

    public int Id { get; init; }
    public int RaceId { get; init; }
    public int RaceDayId { get; init; }
    public int RacerId { get; init; }
    public float LapTimeSeconds { get; init; }

    #endregion
}