using RaceDay.Domain.Entities;
using RaceDay.Domain.Exceptions;
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
        const string selectSql = "SELECT RaceId, RaceDayId, RacerId FROM RaceRacers WHERE rowid = last_insert_rowid();";
        string insertSql = $"INSERT INTO RaceRacers (RaceId, RaceDayId, RacerId) VALUES ({entity.RaceId}, {entity.RaceDayId}, {entity.RacerId});";

        try
        {
            using var cnx = CreateConnection();
            _ = cnx.Query<RaceRacerEntity>(insertSql);
            var result = cnx.Query<RaceRacerEntity>(selectSql)
                            .FirstOrDefault();
            
            if(result == null) throw new Exception("Race Racer not found");

            return result;
        }
        catch (Exception e)
        {
            throw new CreateRecordException("Error creating Race Racer", e);
        }
    }
}