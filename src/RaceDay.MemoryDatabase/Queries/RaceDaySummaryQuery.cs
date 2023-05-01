using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Infrastructure;

namespace RaceDay.MemoryDatabase.Queries;

public class RaceDaySummaryQuery : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public RaceDaySummaryQuery(InMemoryDatabase database) : base(database)
    {
    }

    #endregion


    public IEnumerable<RaceDaySummaryDto> GetAll()
    {
        var raceDays = Database.RaceDays?.GetEntities()
                               .ToArray() ?? Array.Empty<RaceDayEntity>();
        var races = Database.Races?.GetEntities()
                            .ToArray() ?? Array.Empty<RaceEntity>();
        var racers = Database.Racers?.GetEntities()
                             .ToArray() ?? Array.Empty<RacerEntity>();
        var raceLaps = Database.RaceLaps?.GetEntities()
                               .ToArray() ?? Array.Empty<RaceLapEntity>();

        return (from raceDay in raceDays
                let raceDayRaces = races.Where(r => r.RaceDayId == raceDay.Id)
                                        .ToArray()
                let raceDayLaps = raceLaps.Where(rl => rl.RaceDayId == raceDay.Id)
                                          .ToArray()
                let recordLap = raceDayLaps.MinBy(rl => rl.LapTimeSeconds)
                let recordHolder = racers.FirstOrDefault(r => r.Id == recordLap?.RacerId)
                let totalIncome = raceDayRaces.Length * raceDay.SignUpFee
                let totalCost = raceDayLaps.Length * raceDay?.PetrolCostPerLap
                let totalProfit = totalIncome - totalCost
                let averageProfit = totalProfit / raceDayRaces.Length
                select new RaceDaySummaryDto
                {
                    Id = raceDay.Id,
                    Name = raceDay.Name,
                    SignUpFee = raceDay.SignUpFee,
                    LapDistanceKilometers = raceDay?.LapDistanceKm ?? 0,
                    PetrolCostPerLap = raceDay?.PetrolCostPerLap ?? 0,
                    TotalRaces = raceDayRaces.Length,
                    RecordLap = TimeSpan.FromSeconds(recordLap?.LapTimeSeconds ?? 0),
                    RecordHolderName = recordHolder?.Name,
                    AverageProfit = averageProfit ?? 0
                }).ToList();
    }
}