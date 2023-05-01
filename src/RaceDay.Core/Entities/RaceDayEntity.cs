using RaceDay.Core.Interfaces;

namespace RaceDay.Core.Entities;

public class RaceDayEntity : IEntity
{
    public Guid Guid { get; init; }
    public string? Name { get; set; }
    public float SignUpFee { get; init; }
}