using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;

namespace RaceDay.MemoryDatabase.Queries;

public class RaceDaySummaryQuery : QueryBase
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
        var laps = Database.Laps?.GetEntities()
                           .ToArray() ?? Array.Empty<LapEntity>();
        var races = Database.Races?.GetEntities()
                            .ToArray() ?? Array.Empty<RaceEntity>();
        var racers = Database.Racers?.GetEntities()
                             .ToArray() ?? Array.Empty<RacerEntity>();
        var raceLaps = Database.RaceLaps?.GetEntities()
                               .ToArray() ?? Array.Empty<RaceLapEntity>();

        return (from raceDay in raceDays
                let lap = laps.FirstOrDefault(l => l.RaceDayId == raceDay.Id)
                let raceDayRaces = races.Where(r => r.RaceDayId == raceDay.Id)
                                        .ToArray()
                let raceDayLaps = raceLaps.Where(rl => rl.RaceDayId == raceDay.Id)
                                          .ToArray()
                let recordLap = raceDayLaps.MinBy(rl => rl.LapTimeSeconds)
                let recordHolder = racers.FirstOrDefault(r => r.Id == recordLap?.RacerId)
                let totalIncome = raceDayRaces.Length * raceDay.SignUpFee
                let totalCost = raceDayLaps.Length * lap?.PetrolCostPerLap
                let totalProfit = totalIncome - totalCost
                let averageProfit = totalProfit / raceDayRaces.Length
                select new RaceDaySummaryDto
                {
                    Id = raceDay.Id,
                    Name = raceDay.Name,
                    SignUpFee = raceDay.SignUpFee,
                    LapDistanceKilometers = lap?.LapDistanceKm ?? 0,
                    PetrolCostPerLap = lap?.PetrolCostPerLap ?? 0,
                    TotalRaces = raceDayRaces.Length,
                    RecordLap = TimeSpan.FromSeconds(recordLap?.LapTimeSeconds ?? 0),
                    RecordHolderName = recordHolder?.Name,
                    AverageProfit = averageProfit ?? 0
                }).ToList();
    }
}