using RaceDay.Domain.Entities;

namespace RaceDay.Domain.Interfaces;

public interface ICreateRacerLapCommand : IExchangeDataCommand<RaceLapEntity>
{
}