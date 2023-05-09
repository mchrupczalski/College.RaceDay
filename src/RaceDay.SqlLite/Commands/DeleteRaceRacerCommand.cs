using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class DeleteRaceRacerCommand : CommandQueryBase
{
    /// <inheritdoc />
    public DeleteRaceRacerCommand(string dbPath) : base(dbPath)
    {
    }

    public bool Execute(int raceId, int racerId)
    {
        string deleteLapsSql = $"DELETE FROM RaceLaps WHERE RaceId = {raceId} AND RacerId = {racerId};";
        string selectLapsSql = $"SELECT * FROM RaceLaps WHERE RaceId = {raceId} AND RacerId = {racerId};";
        
        string deleteRaceRacerSql = $"DELETE FROM RaceRacers WHERE RaceId = {raceId} AND RacerId = {racerId};";
        string selectRaceRacerSql = $"SELECT * FROM RaceRacers WHERE RaceId = {raceId} AND RacerId = {racerId};";
        
        using var cnx = CreateConnection();
        _ = cnx.Query<RaceLapEntity>(deleteLapsSql);
        bool lapsRemained = cnx.Query<RaceLapEntity>(selectLapsSql)
                       .Any();
        
        if(lapsRemained) throw new Exception("Cannot delete racer because laps remain");

        _ = cnx.Query<RaceRacerEntity>(deleteRaceRacerSql);
        bool raceRacerRemained = cnx.Query<RaceRacerEntity>(selectRaceRacerSql)
                               .Any();
        
        return !raceRacerRemained;
    }
}