namespace RaceDay.Domain.Entities;

public record RacerEntity : EntityBase
{
    #region Properties
    public string? Name { get; init; }
    public byte Age { get; init; }

    #endregion
}