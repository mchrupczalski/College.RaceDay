namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents a Race
/// </summary>
public record RaceDto
{
    #region Properties

    /// <summary>
    ///     The unique identifier of the Race Day
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The name of the Race Day
    /// </summary>
    public string? RaceDayName { get; init; }

    /// <summary>
    ///     The number of the race within the Race Day
    /// </summary>
    public int RaceNumber { get; init; }

    /// <summary>
    ///     The date of the Race
    /// </summary>
    public DateTime RaceDate { get; init; }

    /// <summary>
    ///     The Race sign up fee
    /// </summary>
    public float SignUpFee { get; init; }

    /// <summary>
    ///     The all time lap record in seconds
    /// </summary>
    public float AllTimeLapRecordSeconds { get; init; }

    /// <summary>
    ///     A collection of Racers participating in the Race
    /// </summary>
    public IEnumerable<RacerDto>? Racers { get; init; }

    /// <summary>
    ///     The best time in the Race in seconds
    /// </summary>
    public float RaceLapRecordSeconds { get; init; }

    /// <summary>
    ///     Indicates whether the All Time Lap Record has been beaten within the current race
    /// </summary>
    public bool IsRecordBeaten { get; init; }

    #endregion
}