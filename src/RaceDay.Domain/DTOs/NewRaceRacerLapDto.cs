namespace RaceDay.Domain.DTOs;

public record NewRaceRacerLapDto
{
    public int RaceId { get; init; }
    public int RacerId { get; init; }
    public float LapTimeSeconds { get; init; }
}