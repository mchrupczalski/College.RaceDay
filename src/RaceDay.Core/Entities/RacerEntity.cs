using RaceDay.Core.Interfaces;

namespace RaceDay.Core.Entities;

public class RacerEntity : IEntity
{
    public Guid Guid { get; init; }
    public string Name { get; set; }
    public sbyte Age { get; set; }
}