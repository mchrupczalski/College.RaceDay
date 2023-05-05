using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRaceDayRaceCommand : CommandQueryBase
{
    /// <inheritdoc />
    public CreateRaceDayRaceCommand(InMemoryDatabase database) : base(database)
    {
    }

    public void Execute(NewRaceDto dto)
    {
        int raceNumber = Database.Races?.GetEntities()
                                 .Count(r => r.RaceDayId == dto.RaceDayId) +1  ?? 1;
        var entity = new RaceEntity()
        {
            RaceDayId = dto.RaceDayId,
            RaceNumber = raceNumber,
            RaceDate = dto.RaceDate
        };
        
        Database.Races?.AddEntity(entity);
        
        var raceDayEntity = Database.RaceDays?.GetById(dto.RaceDayId);
        
        return new RaceDto()
        {
            RaceDayId = entity.RaceDayId,
            RaceNumber = raceNumber,
            RaceDate = entity.RaceDate
        };
    }
}