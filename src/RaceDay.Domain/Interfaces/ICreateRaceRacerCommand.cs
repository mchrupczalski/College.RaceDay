using RaceDay.Domain.Entities;
using RaceDay.Domain.Exceptions;

namespace RaceDay.Domain.Interfaces;

public interface ICreateRaceRacerCommand : IExchangeDataCommand<RaceRacerEntity?>
{
}