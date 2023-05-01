namespace RaceDay.Domain.Entities;

public record RaceEntity
{
    #region Properties

    public int Id { get; init; }
    public int RaceDayId { get; init; }

    public DateTime RaceDate { get; init; }

    #endregion
}