using RaceDay.Domain.Entities;
using RaceDay.Domain.Exceptions;

namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A command for creating Racer for a Race.
/// </summary>
public interface ICreateRaceRacerCommand : IExchangeDataCommand<RaceRacerEntity?>
{
}