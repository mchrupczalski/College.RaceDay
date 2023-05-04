using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Infrastructure;

namespace RaceDay.MemoryDatabase.Commands;

public class CreateRaceDayRaceCommand : CommandQueryBase
{
    /// <inheritdoc />
    public CreateRaceDayRaceCommand(InMemoryDatabase database) : base(database)
    {
    }

    public NewRaceDayDto Execute(NewRaceDayDto dto)
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
        
        return new NewRaceDayDto()
        {
            RaceDayId = entity.RaceDayId,
            RaceNumber = raceNumber,
            RaceDate = entity.RaceDate
        };
    }
}