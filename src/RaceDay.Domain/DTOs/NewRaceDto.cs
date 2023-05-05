namespace RaceDay.Domain.DTOs;

public record NewRaceDto
{
    public int RaceDayId { get; init; }
    public int RaceNumber { get; init; }
    public DateTime RaceDate { get; init; }
}