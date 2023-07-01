using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRacerLapCommand : CommandQueryBase, ICreateRacerLapCommand
{
    /// <inheritdoc />
    public CreateRacerLapCommand(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    ///     Creates a new Lap record and returns the new record
    /// </summary>
    /// <param name="entity">Lap to create</param>
    public RaceLapEntity? Execute(RaceLapEntity entity)
    {
        const string selectSql = "SELECT Id, RaceId, RaceDayId, RacerId, LapTimeSeconds FROM RaceLaps WHERE Id = last_insert_rowid();";
        string insertSql = $"INSERT INTO RaceLaps(RaceId, RaceDayId, RacerId, LapTimeSeconds) VALUES({entity.RaceId},{entity.RaceDayId},{entity.RacerId}, {entity.LapTimeSeconds});";

        using var cnx = CreateConnection();

        _ = cnx.Query<RaceLapEntity>(insertSql);
        var result = cnx.Query<RaceLapEntity>(selectSql)
                        .FirstOrDefault();

        return result;
    }
}