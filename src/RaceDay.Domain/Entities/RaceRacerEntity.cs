namespace RaceDay.Domain.Entities;

/// <summary>
///     Represents the RaceRacer Entity
/// </summary>
public record RaceRacerEntity
{
    #region Properties

    /// <summary>
    ///     The Race Id this Racer is participating in
    /// </summary>
    public int RaceId { get; init; }

    /// <summary>
    ///     The Race Day Id this Racer is participating in
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The Racer Id
    /// </summary>
    public int RacerId { get; init; }

    #endregion
}