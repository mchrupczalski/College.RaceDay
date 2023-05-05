using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRacerCommand : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public CreateRacerCommand(string connectionString) : base(connectionString)
    {
    }

    #endregion

    public RacerEntity? Execute(RacerEntity entity)
    {
        // @formatter:off
        const string sql = "INSERT INTO Racers(Name, Age)" +
                           "VALUES(@Name, @Age);" +
                           "SELECT Id, Name, Age" +
                           "FROM Racers" +
                           "WHERE Id = last_insert_rowid();";
        // @formatter:on

        using var cnx = CreateConnection();
        //cnx.Open();
        
        var result = cnx.Query<RacerEntity>(sql)
                        .FirstOrDefault();

        /*
        using var cmd = new SqliteCommand(sql, cnx);
        cmd.Parameters.AddWithValue("@Name", entity.Name);
        cmd.Parameters.AddWithValue("@Age",  entity.Age);
        cmd.Prepare();

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new SqliteException("The record could not be created.", 0);

        var result = new RacerEntity
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Age = reader.GetByte(2)
        };
*/
        return result;
    }
}