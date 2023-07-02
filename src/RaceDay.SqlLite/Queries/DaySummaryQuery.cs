using RaceDay.Domain.DTOs;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

/// <inheritdoc cref="IDaySummaryQuery" />
public class DaySummaryQuery : CommandQueryBase, IDaySummaryQuery
{
    #region Constructors

    /// <inheritdoc />
    public DaySummaryQuery(string connectionString) : base(connectionString) { }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public IEnumerable<DaySummaryDto> GetAll()
    {
        // @formatter:off
        const string sql = "SELECT " +
                           "     RaceDayId" +
                           "    ,RaceDayName" +
                           "    ,SignUpFee" +
                           "    ,LapDistanceKm" +
                           "    ,PetrolCostPerLap" +
                           "    ,TotalRaces" +
                           "    ,RecordLapTime" +
                           "    ,RecordHolderName" +
                           "    ,TotalIncome" +
                           "    ,TotalCost" +
                           " FROM vwDaySummary;";
        // @formatter:on

        using var cnx = CreateConnection();
        var results = cnx.Query<DaySummaryDto>(sql);
        return results;
    }

    /// <inheritdoc />
    public DaySummaryDto? GetById(int id)
    {
        // @formatter:off
        const string sql = "SELECT " +
                           "     RaceDayId" +
                           "    ,RaceDayName" +
                           "    ,SignUpFee" +
                           "    ,LapDistanceKm" +
                           "    ,PetrolCostPerLap" +
                           "    ,TotalRaces" +
                           "    ,RecordLapTime" +
                           "    ,RecordHolderName" +
                           "    ,TotalIncome" +
                           "    ,TotalCost" +
                           " FROM vwDaySummary" +
                           " WHERE RaceDayId = ?;";
        // @formatter:on

        using var cnx = CreateConnection();
        var result = cnx.Query<DaySummaryDto>(sql, id).FirstOrDefault();
        return result;
    }

    #endregion
}