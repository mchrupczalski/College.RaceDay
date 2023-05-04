namespace RaceDay.Domain.DTOs;

public record NewRaceDayDto
{
    public int RaceDayId { get; init; }
    public int RaceNumber { get; init; }
    public DateTime RaceDate { get; init; }
}