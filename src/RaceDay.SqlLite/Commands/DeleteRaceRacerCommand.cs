using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

/// <inheritdoc cref="IDeleteRaceRacerCommand" />
public class DeleteRaceRacerCommand : CommandQueryBase, IDeleteRaceRacerCommand
{
    #region Constructors

    /// <inheritdoc />
    public DeleteRaceRacerCommand(string dbPath) : base(dbPath) { }

    #endregion

    #region Interfaces Implement

    /// <summary>
    ///     Deletes racer from race
    /// </summary>
    /// <param name="raceId">The Race Id to remove the racer from</param>
    /// <param name="racerId">The Racer Id to be removed</param>
    /// <returns>True if the racer was deleted, false if not</returns>
    /// <exception cref="Exception">Throws if laps remain for racer</exception>
    public bool Execute(int raceId, int racerId)
    {
        string deleteLapsSql = $"DELETE FROM RaceLaps WHERE RaceId = {raceId} AND RacerId = {racerId};";
        string selectLapsSql = $"SELECT * FROM RaceLaps WHERE RaceId = {raceId} AND RacerId = {racerId};";

        string deleteRaceRacerSql = $"DELETE FROM RaceRacers WHERE RaceId = {raceId} AND RacerId = {racerId};";
        string selectRaceRacerSql = $"SELECT * FROM RaceRacers WHERE RaceId = {raceId} AND RacerId = {racerId};";

        using var cnx = CreateConnection();
        _ = cnx.Query<RaceLapEntity>(deleteLapsSql);
        bool lapsRemained = cnx.Query<RaceLapEntity>(selectLapsSql).Any();

        if (lapsRemained)
            throw new Exception("Cannot delete racer because laps remain");

        _ = cnx.Query<RaceRacerEntity>(deleteRaceRacerSql);
        bool raceRacerRemained = cnx.Query<RaceRacerEntity>(selectRaceRacerSql).Any();

        return !raceRacerRemained;
    }

    #endregion
}