using RaceDay.Domain.Entities;
using RaceDay.Domain.Exceptions;
using RaceDay.SqlLite.Infrastructure;
using SQLite;

namespace RaceDay.SqlLite.Commands;

public class CreateRacerCommand : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public CreateRacerCommand(string connectionString) : base(connectionString)
    {
    }

    #endregion

    public RacerEntity Execute(RacerEntity entity)
    {
        const string selectSql = "SELECT Id, Name, Age FROM Racers WHERE rowid = last_insert_rowid();";
        string insertSql = $"INSERT INTO Racers(Name, Age) VALUES('{entity.Name}', {entity.Age});";

        try
        {
            using var cnx = CreateConnection();
            _ = cnx.Query<RacerEntity>(insertSql);
            var result = cnx.Query<RacerEntity>(selectSql)
                            .FirstOrDefault();
            
            if(result == null) throw new Exception("Racer not found");

            return result;
        }
        catch (Exception e)
        {
            throw new CreateRecordException("Error creating racer", e);
        }

    }
}