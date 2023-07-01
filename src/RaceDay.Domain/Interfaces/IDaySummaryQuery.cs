using RaceDay.Domain.DTOs;

namespace RaceDay.Domain.Interfaces;

public interface IDaySummaryQuery
{
    /// <summary>
    ///     Gets all Day Summaries
    /// </summary>
    IEnumerable<DaySummaryDto> GetAll();

    DaySummaryDto? GetById(int id);
}