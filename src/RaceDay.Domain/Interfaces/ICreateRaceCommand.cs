using RaceDay.Domain.Entities;

namespace RaceDay.Domain.Interfaces;

public interface ICreateRaceCommand : IExchangeDataCommand<RaceEntity?>
{
}