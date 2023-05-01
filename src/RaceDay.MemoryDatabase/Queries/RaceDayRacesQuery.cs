using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Infrastructure;

namespace RaceDay.MemoryDatabase.Queries;

public class RaceDayRacesQuery : CommandQueryBase
{
    /// <inheritdoc />
    public RaceDayRacesQuery(InMemoryDatabase database) : base(database)
    {
    }

    public IEnumerable<RaceDayRaceDto> GetAll(int raceDayId)
    {
        var raceDay = Database.RaceDays?.GetEntities()
                              .FirstOrDefault(rd => rd.Id == raceDayId) ?? throw new ArgumentNullException(nameof(raceDayId), "Race Day not found");
        
        var races = Database.Races?.GetEntities()
                            .Where(r => r.RaceDayId == raceDayId)
                            .ToArray() ?? Array.Empty<RaceEntity>();
        var racers = Database.Racers?.GetEntities()
                             .ToArray() ?? Array.Empty<RacerEntity>();
        
        var laps = Database.RaceLaps?.GetEntities()
                           .ToArray() ?? Array.Empty<RaceLapEntity>();

        return (from race in races
                let raceLaps = laps.Where(rl => rl.RaceNumber == race.RaceNumber)
                                   .ToArray()
                let uniqueRacersCount = raceLaps.Select(rl => rl.RacerId)
                                                .Distinct()
                                                .Count()
                let bestLap = raceLaps.MinBy(rl => rl.LapTimeSeconds)
                let bestLapRacer = racers.FirstOrDefault(r => r.Id == bestLap?.RacerId)
                let totalIncome = raceDay.SignUpFee * uniqueRacersCount
                let totalExpense = raceDay.PetrolCostPerLap * raceLaps.Length
                let totalProfit = totalIncome - totalExpense
                select new RaceDayRaceDto()
                {
                    RaceDayId = raceDayId,
                    RaceNumber = race.RaceNumber,
                    RaceDate = race.RaceDate,
                    NumberOfRacers = uniqueRacersCount,
                    TotalNumberOfLaps = raceLaps.Length,
                    BestLapTime = TimeSpan.FromSeconds(bestLap?.LapTimeSeconds ?? 0),
                    BestLapTimeHolder = bestLapRacer?.Name,
                    TotalIncome = totalIncome,
                    TotalExpenses = totalExpense,
                    TotalProfit = totalProfit
                }).ToList();
    }
}