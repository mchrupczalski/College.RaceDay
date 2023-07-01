namespace RaceDay.Domain.Interfaces;

public interface IDeleteRaceRacerCommand
{
    /// <summary>
    ///     Deletes racer from race
    /// </summary>
    /// <param name="raceId">The Race Id to remove the racer from</param>
    /// <param name="racerId">The Racer Id to be removed</param>
    /// <returns>True if the racer was deleted, false if not</returns>
    /// <exception cref="Exception">Throws if laps remain for racer</exception>
    bool Execute(int raceId, int racerId);
}