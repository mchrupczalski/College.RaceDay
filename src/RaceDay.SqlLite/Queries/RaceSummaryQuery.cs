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
        // @formatter:off
        const string sql = "SELECT" +
                           "    RaceDayId" +
                           "    ,RaceDate" +
                           "    ,TotalRacers" +
                           "    ,TotalLaps" +
                           "    ,BestLapTime" +
                           "    ,BestLapTimeHolder" +
                           "    ,TotalIncome" +
                           "    ,TotalExpense" +
                           " FROM vwRaceSummary" +
                           " WHERE RaceDayId = ?;";
        // @formatter:on

        using var cnx = CreateConnection();
        //cnx.Open();
        
        var results = cnx.Query<RaceSummaryDto>(sql, raceDayId);
/*
        using var cmd = new SqliteCommand(sql, cnx);
        cmd.Parameters.AddWithValue("@RaceDayId", raceDayId);
        cmd.Prepare();

        var results = new List<RaceSummaryDto>();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
            results.Add(new RaceSummaryDto
            {
                RaceDayId = reader.GetInt32(0),
                RaceDate = DateTime.Parse(reader.GetString(1)),
                TotalRacers = reader.GetInt32(2),
                TotalLaps = reader.GetInt32(3),
                BestLapTime = TimeSpan.FromSeconds(reader.GetFloat(4)),
                BestLapTimeHolder = reader.GetString(5),
                TotalIncome = reader.GetFloat(6),
                TotalExpense = reader.GetFloat(7)
            });
*/
        return results;
    }
}