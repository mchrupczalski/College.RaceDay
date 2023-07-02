using RaceDay.Domain.DTOs;

namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A query for getting Racers.
/// </summary>
public interface IRacersQuery
{
    #region Abstract Members

    /// <summary>
    ///     Gets all Racers
    /// </summary>
    IEnumerable<RacerDto> GetAll();

    /// <summary>
    ///     Gets all Racers for Race
    /// </summary>
    /// <param name="raceId">The Race Id</param>
    IEnumerable<RacerDto> GetRacersForRace(int raceId);

    #endregion
}