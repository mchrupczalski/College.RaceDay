namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents a new Race
/// </summary>
public record NewRaceDto
{
    #region Properties

    /// <summary>
    ///     The unique identifier for the RaceDay
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The data of the Race
    /// </summary>
    public DateTime RaceDate { get; init; }

    #endregion
}