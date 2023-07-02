using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

/// <inheritdoc cref="IDeleteRaceLapCommand" />
public class DeleteRaceLapCommand : CommandQueryBase, IDeleteRaceLapCommand
{
    #region Constructors

    /// <inheritdoc />
    public DeleteRaceLapCommand(string dbPath) : base(dbPath) { }

    #endregion

    #region Interfaces Implement

    /// <summary>
    ///     Deletes the lap for racer
    /// </summary>
    /// <param name="raceLapId">Id of the lap to delete</param>
    /// <returns>True if the lap was deleted, false if not</returns>
    public bool Execute(int raceLapId)
    {
        string deleteSql = $"DELETE FROM RaceLaps WHERE Id = {raceLapId};";
        string selectSql = $"SELECT * FROM RaceLaps WHERE Id = {raceLapId};";

        using var cnx = CreateConnection();
        _ = cnx.Query<RaceLapEntity>(deleteSql);
        var result = cnx.Query<RaceLapEntity>(selectSql).FirstOrDefault();

        return result == null;
    }

    #endregion
}