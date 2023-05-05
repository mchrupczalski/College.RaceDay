namespace RaceDay.Domain.Entities;

public record RaceRacerEntity : EntityBase
{
    public int RaceDayId { get; init; }
    public int RaceNumber { get; init; }
    public int RacerId { get; init; }
}