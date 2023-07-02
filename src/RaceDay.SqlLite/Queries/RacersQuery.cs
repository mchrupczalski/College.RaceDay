using RaceDay.Domain.DTOs;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Queries;

/// <inheritdoc cref="IRacersQuery" />
public class RacersQuery : CommandQueryBase, IRacersQuery
{
    #region Constructors

    /// <inheritdoc />
    public RacersQuery(string dbPath) : base(dbPath) { }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public IEnumerable<RacerDto> GetAll()
    {
        const string sql = "SELECT Id, Name, Age FROM Racers";
        using var cnx = CreateConnection();
        return cnx.Query<RacerDto>(sql);
    }

    /// <inheritdoc />
    public IEnumerable<RacerDto> GetRacersForRace(int raceId)
    {
        const string sql = "SELECT Id, Name, Age FROM vwRaceRacers WHERE RaceId = ?";
        using var cnx = CreateConnection();
        return cnx.Query<RacerDto>(sql, raceId);
    }

    #endregion
}