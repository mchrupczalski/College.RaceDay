using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRacerLapCommand : CommandQueryBase
{
    /// <inheritdoc />
    public CreateRacerLapCommand(string dbPath) : base(dbPath)
    {
    }

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