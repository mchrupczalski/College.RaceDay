using RaceDay.Domain.DTOs;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

public class DaySummaryQuery : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public DaySummaryQuery(string connectionString) : base(connectionString)
    {
    }

    #endregion


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
        //cnx.Open();

        var results = cnx.Query<DaySummaryDto>(sql);
/*
        var cmd = new SQLiteCommand(cnx);
        cmd.CommandText = sql;
        //cmd.Prepare();

        var results = new List<DaySummaryDto>();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
            results.Add(new DaySummaryDto
            {
                RaceDayId = reader.GetInt32(0),
                RaceDayName = reader.GetString(1),
                SignUpFee = reader.GetFloat(2),
                LapDistanceKm = reader.GetFloat(3),
                PetrolCostPerLap = reader.GetFloat(4),
                TotalRaces = reader.GetInt32(5),
                RecordLapTime = TimeSpan.FromSeconds(reader.GetFloat(6)),
                RecordHolderName = reader.GetString(7),
                TotalIncome = reader.GetFloat(8),
                TotalCost = reader.GetFloat(9)
            });
*/
        return results;
    }
}