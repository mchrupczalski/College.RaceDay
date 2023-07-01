using RaceDay.Domain.DTOs;

namespace RaceDay.Domain.Interfaces;

public interface IRacerLapQuery
{
    /// <summary>
    ///     Gets all Laps for Racer in Race
    /// </summary>
    /// <param name="raceId">The Race Id</param>
    /// <param name="racerId">The Racer Id</param>
    IEnumerable<RaceLapDto> GetLapsForRacerInRace(int raceId, int racerId);
}