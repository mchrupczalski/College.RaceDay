namespace RaceDay.Domain.Entities;

public record RaceDayEntity
{
    #region Properties

    public int Id { get; init; }
    public string? Name { get; init; }
    public float SignUpFee { get; init; }

    public float LapDistanceKm { get; init; }
    public float PetrolCostPerLap { get; init; }

    #endregion
}