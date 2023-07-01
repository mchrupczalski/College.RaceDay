using RaceDay.Domain.DTOs;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

public class RacersQuery : CommandQueryBase, IRacersQuery
{
    /// <inheritdoc />
    public RacersQuery(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    ///     Gets all Racers
    /// </summary>
    public IEnumerable<RacerDto> GetAll()
    {
        const string sql = "SELECT Id, Name, Age FROM Racers";
        using var cnx = CreateConnection();
        return cnx.Query<RacerDto>(sql);
    }

    /// <summary>
    ///     Gets all Racers for Race    
    /// </summary>
    /// <param name="raceId">The Race Id</param>
    public IEnumerable<RacerDto> GetRacersForRace(int raceId)
    {
        const string sql = "SELECT Id, Name, Age FROM vwRaceRacers WHERE RaceId = ?";
        using var cnx = CreateConnection();
        return cnx.Query<RacerDto>(sql, raceId);
    }
}