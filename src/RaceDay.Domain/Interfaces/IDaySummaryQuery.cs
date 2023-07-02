using RaceDay.Domain.DTOs;

namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A query for getting Day Summaries.
/// </summary>
public interface IDaySummaryQuery
{
    #region Abstract Members

    /// <summary>
    ///     Gets all Day Summaries
    /// </summary>
    IEnumerable<DaySummaryDto> GetAll();

    /// <summary>
    ///     Gets a Day Summary by Id
    /// </summary>
    /// <param name="id">Id of the Day Summary to get</param>
    /// <returns>Day Summary with the given Id or null</returns>
    DaySummaryDto? GetById(int id);

    #endregion
}