using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Infrastructure;

namespace RaceDay.MemoryDatabase.Commands;

public class CreateRaceDayCommand : CommandQueryBase
{
    /// <inheritdoc />
    public CreateRaceDayCommand(InMemoryDatabase database) : base(database)
    {
    }

    public void Create(RaceDayDto dto)
    {
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