namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents a RaceSummary
/// </summary>
public record RaceSummaryDto
{
    #region Properties

    /// <summary>
    ///     A unique identifier for the Race
    /// </summary>
    public int RaceId { get; init; }

    /// <summary>
    ///     A unique identifier for the RaceDay
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The date of the Race
    /// </summary>
    public string? RaceDate { get; init; }

    /// <summary>
    ///     A total number of Racers participating in the Race
    /// </summary>
    public int TotalRacers { get; init; }

    /// <summary>
    ///     A total number of Laps completed by all Racers during the Race
    /// </summary>
    public int TotalLaps { get; init; }

    /// <summary>
    ///     The best Lap time in seconds, for the current Race
    /// </summary>
    public float BestLapTime { get; init; }

    /// <summary>
    ///     The name of the Racer who holds the <see cref="BestLapTime" />
    /// </summary>
    public string? BestLapTimeHolder { get; init; }

    /// <summary>
    ///     The total income made from the Race
    /// </summary>
    public float TotalIncome { get; init; }

    /// <summary>
    ///     The total cost incurred in the race
    /// </summary>
    public float TotalExpense { get; init; }

    #endregion
}