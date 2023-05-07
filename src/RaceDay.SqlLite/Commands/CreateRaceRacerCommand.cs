using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRaceRacerCommand : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public CreateRaceRacerCommand(string connectionString) : base(connectionString)
    {
    }

    #endregion

    public RaceRacerEntity? Execute(RaceRacerEntity entity)
    {
        // @formatter:off
        const string sql = @"INSERT INTO RaceRacers (RaceId, RaceDayId, RacerId)" + 
                           "VALUES (@RaceId, @RaceDayId, @RacerId);" +
                           "SELECT RaceId, RaceDayId, RacerId" +
                           "FROM RaceRacers" +
                           "WHERE Id = last_insert_rowid();";
        // @formatter:on

        using var cnx = CreateConnection();
        //cnx.Open();
        
        var result = cnx.Query<RaceRacerEntity>(sql)
                        .FirstOrDefault();
/*
        using var cmd = new SqliteCommand(sql, cnx);
        cmd.Parameters.AddWithValue("@RaceId",    entity.RaceId);
        cmd.Parameters.AddWithValue("@RaceDayId", entity.RaceDayId);
        cmd.Parameters.AddWithValue("@RacerId",   entity.RacerId);
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new SqliteException("The record could not be created.", 0);

        var result = new RaceRacerEntity
        {
            RaceId = reader.GetInt32(0),
            RaceDayId = reader.GetInt32(1),
            RacerId = reader.GetInt32(2)
        };
*/
        return result;
    }
}