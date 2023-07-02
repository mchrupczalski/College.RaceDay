namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents a new RaceDay
/// </summary>
public record NewRaceDayDto
{
    #region Properties

    /// <summary>
    ///     The name of the RaceDay
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     The cost to sign up for the RaceDay
    /// </summary>
    public float SignUpFee { get; init; }

    /// <summary>
    ///     The distance of each lap in kilometers for the RaceDay
    /// </summary>
    public float LapDistanceKm { get; init; }

    /// <summary>
    ///     The petrol cost per lap for the RaceDay
    /// </summary>
    public float PetrolCostPerLap { get; init; }

    #endregion
}