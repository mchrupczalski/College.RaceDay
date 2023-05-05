namespace RaceDay.Domain.DTOs;

public record NewRaceDayDto
{
    #region Properties
    
    public string? Name { get; init; }
    public float SignUpFee { get; init; }
    public float LapDistanceKm { get; init; }
    public float PetrolCostPerLap { get; init; }

    #endregion
}