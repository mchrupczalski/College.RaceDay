namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents a RaceLap
/// </summary>
public record RaceLapDto
{
    #region Properties

    /// <summary>
    ///     A unique identifier for the RaceLap
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    ///     The Id of the Race
    /// </summary>
    public int RaceId { get; init; }

    /// <summary>
    ///     The Id of the RaceDay
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The Id of the Racer
    /// </summary>
    public int RacerId { get; init; }

    /// <summary>
    ///     The Lap time in seconds
    /// </summary>
    public float LapTimeSeconds { get; init; }

    #endregion
}