using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.Domain.Exceptions;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

public class CreateRaceDayCommand : CommandQueryBase
{
    /// <inheritdoc />
    public CreateRaceDayCommand(InMemoryDatabase database) : base(database)
    {
    }

    public RaceDaySummaryDto Execute(NewRaceDayDto dto)
    {
        var existingRaceDay = Database.RaceDays?.GetEntities().FirstOrDefault(x => x.Name == dto.Name);
        if (existingRaceDay != null) throw new RecordExistException($"Race Day with name '{dto.Name}' already exists.");
        
        var entity = new RaceDayEntity
        {
            Name = dto.Name,
            SignUpFee = dto.SignUpFee,
            LapDistanceKm = dto.LapDistanceKm,
            PetrolCostPerLap = dto.PetrolCostPerLap
        };
        
        Database.RaceDays?.AddEntity(entity);
    }
}