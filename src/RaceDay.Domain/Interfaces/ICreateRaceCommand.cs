using RaceDay.Domain.Entities;

namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A command for creating Race.
/// </summary>
public interface ICreateRaceCommand : IExchangeDataCommand<RaceEntity?> { }