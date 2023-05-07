namespace RaceDay.Domain.Entities;

/// <summary>
///     Represents the Race Entity
/// </summary>
public record RaceEntity : EntityBase
{
    #region Properties

    /// <summary>
    ///     The Race Day Id to which the race belongs.
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The date of the Race
    /// </summary>
    public DateTime RaceDate { get; init; }

    #endregion
}