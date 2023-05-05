using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRaceLapCommand : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public CreateRaceLapCommand(string connectionString) : base(connectionString)
    {
    }

    #endregion

    public RaceLapEntity? Execute(RaceLapEntity entity)
    {
        // @formatter:off
        const string sql = "INSERT INTO RaceLaps(RaceId, RaceDayId, RacerId, LapTimeSeconds)" + 
                           "VALUES(@RaceId, @RaceDayId, @RacerId, @TimeS);" +
                           "SELECT Id, RaceId, RaceDayId, RacerId, LapTimeSeconds" +
                           "FROM RaceLaps" +
                           "WHERE Id = last_insert_rowid();";
        // @formatter:on

        using var cnx = CreateConnection();
        //cnx.Open();

        var result = cnx.Query<RaceLapEntity>(sql)
                        .FirstOrDefault();

        /*

        using var cmd = new SqliteCommand(sql, cnx);
        cmd.Parameters.AddWithValue("@RaceId",    entity.RaceId);
        cmd.Parameters.AddWithValue("@RaceDayId", entity.RaceDayId);
        cmd.Parameters.AddWithValue("@RacerId",   entity.RacerId);
        cmd.Parameters.AddWithValue("@TimeS",     entity.LapTimeSeconds);
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new SqliteException("The record could not be created.", 0);

        var result = new RaceLapEntity
        {
            Id = reader.GetInt32(0),
            RaceId = reader.GetInt32(1),
            RaceDayId = reader.GetInt32(2),
            RacerId = reader.GetInt32(3),
            LapTimeSeconds = reader.GetFloat(4)
        };
*/
        return result;
    }
}