using RaceDay.Domain.DTOs;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

public class RaceSummaryQuery : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public RaceSummaryQuery(string connectionString) : base(connectionString)
    {
    }

    #endregion


    /// <summary>
    ///     Gets all Races for Race Day summaries
    /// </summary>
    /// <param name="raceDayId">The Race Day id</param>
    public IEnumerable<RaceSummaryDto> GetAll(int raceDayId)
    {
        const string sql = "SELECT RaceId, RaceDayId, RaceDate, TotalRacers, TotalLaps, BestLapTime, BestLapTimeHolder, TotalIncome, TotalExpense" +
                           " FROM vwRaceSummary" + 
                           " WHERE RaceDayId = ?;";

        using var cnx = CreateConnection();
        var results = cnx.Query<RaceSummaryDto>(sql, raceDayId);
        return results;
    }

    /// <summary>
    ///     Gets summary for a specific Race
    /// </summary>
    /// <param name="id">The Race Id</param>
    public RaceSummaryDto? GetById(int id)
    {
        // @formatter:off
        const string sql = "SELECT" +
                           "    RaceId" +
                           "    ,RaceDayId" +
                           "    ,RaceDate" +
                           "    ,TotalRacers" +
                           "    ,TotalLaps" +
                           "    ,BestLapTime" +
                           "    ,BestLapTimeHolder" +
                           "    ,TotalIncome" +
                           "    ,TotalExpense" +
                           " FROM vwRaceSummary" +
                           " WHERE RaceId = ?;";
        // @formatter:on

        using var cnx = CreateConnection();
        var result = cnx.Query<RaceSummaryDto>(sql, id)
                        .FirstOrDefault();
        return result;
    }
}