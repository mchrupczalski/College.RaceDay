namespace RaceDay.Domain.Entities;

public record RacerEntity
{
    #region Properties

    public int Id { get; init; }
    public string? Name { get; init; }
    public byte Age { get; init; }

    #endregion
}