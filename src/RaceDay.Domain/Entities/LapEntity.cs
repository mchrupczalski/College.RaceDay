namespace RaceDay.Domain.Entities;

public record LapEntity
{
    #region Properties

    public int RaceDayId { get; init; }
    public float LapDistanceKm { get; init; }
    public float PetrolCostPerLap { get; init; }

    #endregion
}