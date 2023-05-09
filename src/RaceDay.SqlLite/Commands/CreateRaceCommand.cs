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


    /// <summary>
    ///     Creates a new Race record and returns the new record
    /// </summary>
    /// <param name="entity">Race to create</param>
    public RaceEntity? Execute(RaceEntity entity)
    {
        const string selectSql = "SELECT Id, RaceDayId, RaceDate FROM Races WHERE Id = last_insert_rowid();";
        string insertSql = $"INSERT INTO Races(RaceDayId, RaceDate) VALUES({entity.RaceDayId}, '{entity.RaceDate:yyyy/MM/dd}');";

        using var cnx = CreateConnection();

        _ = cnx.Query<RaceEntity>(insertSql);
        var result = cnx.Query<RaceEntity>(selectSql)
                        .FirstOrDefault();

        return result;
    }
}