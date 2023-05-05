using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRaceCommand : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public CreateRaceCommand(string connectionString) : base(connectionString)
    {
    }

    #endregion


    public RaceEntity? Execute(RaceEntity entity)
    {
        // @formatter:off
        const string sql = "INSERT INTO Races(RaceDayId, RaceDate)" +
                           "VALUES(@RaceDayId, @RaceDate);" +
                           "SELECT Id, RaceDayId, RaceDate" +
                           "FROM Races" + 
                           "WHERE Id = last_insert_rowid();";
        // @formatter:on

        using var cnx = CreateConnection();
        //cnx.Open();

        var result = cnx.Query<RaceEntity>(sql)
                        .FirstOrDefault();
/*
        using var cmd = new SqliteCommand(sql, cnx);
        cmd.Parameters.AddWithValue("@RaceDayId", entity.RaceDayId);
        cmd.Parameters.AddWithValue("@RaceDate",  entity.RaceDate.ToString("yyyy-MM-dd"));
        cmd.Prepare();

        var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new SqliteException("The record could not be created.", 0);

        var result = new RaceEntity
        {
            Id = reader.GetInt32(0),
            RaceDayId = reader.GetInt32(1),
            RaceDate = DateTime.Parse(reader.GetString(2))
        };
*/
        return result;
    }
}