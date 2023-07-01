using RaceDay.Domain.DTOs;

namespace RaceDay.Domain.Interfaces;

public interface IRaceSummaryQuery
{
    /// <summary>
    ///     Gets all Races for Race Day summaries
    /// </summary>
    /// <param name="raceDayId">The Race Day id</param>
    IEnumerable<RaceSummaryDto> GetAll(int raceDayId);

    /// <summary>
    ///     Gets summary for a specific Race
    /// </summary>
    /// <param name="id">The Race Id</param>
    RaceSummaryDto? GetById(int id);
}