using RaceDay.Domain.Entities;
using RaceDay.Domain.Exceptions;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

/// <inheritdoc cref="ICreateRaceRacerCommand" />
public class CreateRaceRacerCommand : CommandQueryBase, ICreateRaceRacerCommand
{
    #region Constructors

    /// <inheritdoc />
    public CreateRaceRacerCommand(string connectionString) : base(connectionString) { }

    #endregion

    #region Interfaces Implement

    /// <summary>
    ///     Adds a racer to race and returns the new record
    /// </summary>
    /// <param name="entity">Record to create</param>
    /// <exception cref="Exception">Throws if record not found</exception>
    /// <exception cref="CreateRecordException">Throws if error creating record</exception>
    public RaceRacerEntity? Execute(RaceRacerEntity? entity)
    {
        if (entity == null)
            return entity;

        const string selectSql = "SELECT RaceId, RaceDayId, RacerId FROM RaceRacers WHERE rowid = last_insert_rowid();";
        string insertSql = $"INSERT INTO RaceRacers (RaceId, RaceDayId, RacerId) VALUES ({entity.RaceId}, {entity.RaceDayId}, {entity.RacerId});";

        try
        {
            using var cnx = CreateConnection();
            _ = cnx.Query<RaceRacerEntity>(insertSql);
            var result = cnx.Query<RaceRacerEntity>(selectSql).FirstOrDefault();

            if (result == null)
                throw new Exception("Race Racer not found");

            return result;
        }
        catch (Exception e)
        {
            throw new CreateRecordException("Error creating Race Racer", e);
        }
    }

    #endregion
}