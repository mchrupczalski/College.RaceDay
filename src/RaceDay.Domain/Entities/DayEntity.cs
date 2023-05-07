namespace RaceDay.Domain.Entities;

/// <summary>
///     Represents the Day entity.
/// </summary>
public record DayEntity : EntityBase
{
    #region Properties

    /// <summary>
    ///     The name of the race day.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     The signup fee
    /// </summary>
    public float Fee { get; init; }

    /// <summary>
    ///     The lap distance in kilometers
    /// </summary>
    public float LapDistanceKm { get; init; }

    /// <summary>
    ///     The petrol cost per lap
    /// </summary>
    public float PetrolCostPerLap { get; init; }

    #endregion
}