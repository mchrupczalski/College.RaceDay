namespace RaceDay.Domain.Entities;

public record RaceDayEntity : EntityBase
{
    #region Properties
    
    public string? Name { get; init; }
    public float SignUpFee { get; init; }

    public float LapDistanceKm { get; init; }
    public float PetrolCostPerLap { get; init; }

    #endregion
}