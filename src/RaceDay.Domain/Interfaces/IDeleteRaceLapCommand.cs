namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A command for deleting a Lap for a Racer.
/// </summary>
public interface IDeleteRaceLapCommand
{
    #region Abstract Members

    /// <summary>
    ///     Deletes the lap for racer
    /// </summary>
    /// <param name="raceLapId">Id of the lap to delete</param>
    /// <returns>True if the lap was deleted, false if not</returns>
    bool Execute(int raceLapId);

    #endregion
}