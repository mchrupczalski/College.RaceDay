using RaceDay.Domain.Entities;
using RaceDay.Domain.Exceptions;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;
using SQLite;

namespace RaceDay.SqlLite.Commands;

/// <inheritdoc cref="ICreateRacerCommand" />
public class CreateRacerCommand : CommandQueryBase, ICreateRacerCommand
{
    #region Constructors

    /// <inheritdoc />
    public CreateRacerCommand(string connectionString) : base(connectionString)
    {
    }

    #endregion

    /// <summary>
    ///     Creates a new Racer record and returns the new record
    /// </summary>
    /// <param name="entity">Racer to create</param>
    /// <exception cref="Exception">Thrown if record not found</exception>
    /// <exception cref="CreateRecordException">Thrown if error creating record</exception>
    public RacerEntity Execute(RacerEntity? entity)
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