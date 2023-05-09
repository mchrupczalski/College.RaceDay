using RaceDay.Domain.DTOs;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

public class RacerLapQuery : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public RacerLapQuery(string dbPath) : base(dbPath)
    {
    }

    #endregion

    public IEnumerable<RaceLapDto> GetLapsForRacerInRace(int raceId, int racerId)
    {
        const string sql = "SELECT Id, RaceId, RaceDayId, RacerId, LapTimeSeconds FROM RaceLaps WHERE RaceId = ? AND RacerId = ? ORDER BY Id ASC";
        using var cnx = CreateConnection();
        return cnx.Query<RaceLapDto>(sql, raceId, racerId);
    }
}