using RaceDay.Domain.Entities;

namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A command for creating Racer.
/// </summary>
public interface ICreateRacerCommand : IExchangeDataCommand<RacerEntity?> { }