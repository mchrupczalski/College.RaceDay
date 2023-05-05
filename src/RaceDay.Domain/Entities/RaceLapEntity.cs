namespace RaceDay.Domain.Entities;

/// <summary>
///     Represents the RaceLap entity, which is a record of a lap for a racer within the race.
/// </summary>
public record RaceLapEntity : EntityBase
{
    #region Properties

    /// <summary>
    ///     The Race Id this lap is associated with.
    /// </summary>
    public int RaceId { get; init; }
    
    /// <summary>
    ///     The Race Day Id this Racer is participating in
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The Racer Id this lap is associated with.
    /// </summary>
    public int RacerId { get; init; }

    /// <summary>
    ///     The lap time in seconds.
    /// </summary>
    public float LapTimeSeconds { get; init; }

    #endregion
}