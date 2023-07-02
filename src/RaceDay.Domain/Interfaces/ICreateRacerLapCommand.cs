using RaceDay.Domain.Entities;

namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A command for creating Lap for a Racer.
/// </summary>
public interface ICreateRacerLapCommand : IExchangeDataCommand<RaceLapEntity> { }