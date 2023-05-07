using RaceDay.Domain.DTOs;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

public class RacersQuery : CommandQueryBase
{
    /// <inheritdoc />
    public RacersQuery(string dbPath) : base(dbPath)
    {
    }

    public IEnumerable<RacerDto> GetAll()
    {
        const string sql = "SELECT Id, Name, Age FROM Racers";
        using var cnx = CreateConnection();
        return cnx.Query<RacerDto>(sql);
    }

    public IEnumerable<RacerDto> GetRacersForRace(int raceId)
    {
        const string sql = "SELECT Id, Name, Age FROM vwRaceRacers WHERE RaceId = ?";
        using var cnx = CreateConnection();
        return cnx.Query<RacerDto>(sql, raceId);
    }
}