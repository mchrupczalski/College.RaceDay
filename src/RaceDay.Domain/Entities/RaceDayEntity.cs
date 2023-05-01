namespace RaceDay.Domain.Entities;

public record RaceDayEntity
{
    #region Properties

    public int Id { get; init; }
    public string? Name { get; init; }
    public float SignUpFee { get; init; }

    #endregion
}