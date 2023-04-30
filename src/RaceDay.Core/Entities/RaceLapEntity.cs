using RaceDay.Core.Interfaces;

namespace RaceDay.Core.Entities;

public class RaceLapEntity : IEntity
{
    public Guid Guid { get; init; }
    public Guid RaceGuid { get; init; }
    public Guid LapGuid { get; init; }
    public Guid RacerGuid { get; init; }
    public int LapNumber { get; set; }
    public float LapTimeSeconds { get; set; }
    public float LapSpeed { get; set; }
}