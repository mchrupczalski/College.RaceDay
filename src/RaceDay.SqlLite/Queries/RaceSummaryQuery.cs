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


    public IEnumerable<RaceSummaryDto> GetAll(int raceDayId)
    {
        const string sql = "SELECT RaceId, RaceDayId, RaceDate, TotalRacers, TotalLaps, BestLapTime, BestLapTimeHolder, TotalIncome, TotalExpense" +
                           " FROM vwRaceSummary" + 
                           " WHERE RaceDayId = ?;";

        using var cnx = CreateConnection();
        var results = cnx.Query<RaceSummaryDto>(sql, raceDayId);
        return results;
    }

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