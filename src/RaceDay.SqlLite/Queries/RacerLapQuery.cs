using RaceDay.Domain.DTOs;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

/// <inheritdoc cref="IRacerLapQuery" />
public class RacerLapQuery : CommandQueryBase, IRacerLapQuery
{
    #region Constructors

    /// <inheritdoc />
    public RacerLapQuery(string dbPath) : base(dbPath) { }

    #endregion

    #region Interfaces Implement

    /// <summary>
    ///     Gets all Laps for Racer in Race
    /// </summary>
    /// <param name="raceId">The Race Id</param>
    /// <param name="racerId">The Racer Id</param>
    public IEnumerable<RaceLapDto> GetLapsForRacerInRace(int raceId, int racerId)
    {
        const string sql = "SELECT Id, RaceId, RaceDayId, RacerId, LapTimeSeconds FROM RaceLaps WHERE RaceId = ? AND RacerId = ? ORDER BY Id ASC";
        using var cnx = CreateConnection();
        return cnx.Query<RaceLapDto>(sql, raceId, racerId);
    }

    #endregion
}