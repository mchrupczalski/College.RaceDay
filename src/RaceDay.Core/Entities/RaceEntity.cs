using RaceDay.Core.Interfaces;

namespace RaceDay.Core.Entities;

public class RaceEntity : IEntity
{
    public Guid Guid { get; init; }
    public Guid RaceDayGuid { get; init; }

    public DateTime RaceDate { get; init; }
}