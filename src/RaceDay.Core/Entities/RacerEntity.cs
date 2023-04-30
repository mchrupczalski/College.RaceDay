using RaceDay.Core.Interfaces;

namespace RaceDay.Core.Entities;

public class RacerEntity : IEntity
{
    public Guid Guid { get; init; }
    public string Name { get; set; }
    public byte Age { get; set; }
}