using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class DeleteRaceLapCommand : CommandQueryBase
{
    /// <inheritdoc />
    public DeleteRaceLapCommand(string dbPath) : base(dbPath)
    {
    }
    
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
        var result = cnx.Query<RaceLapEntity>(selectSql)
                        .FirstOrDefault();
        
        return result == null;
    }
}