namespace RaceDay.Domain.Entities;

public record RaceRacerEntity
{
    public int RaceDayId { get; init; }
    public int RaceId { get; init; }
    public int RacerId { get; init; }
}