namespace RaceDay.Domain.Interfaces;

public interface IDeleteRaceLapCommand
{
    /// <summary>
    ///     Deletes the lap for racer
    /// </summary>
    /// <param name="raceLapId">Id of the lap to delete</param>
    /// <returns>True if the lap was deleted, false if not</returns>
    bool Execute(int raceLapId);
}