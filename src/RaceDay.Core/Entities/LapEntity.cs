using RaceDay.Core.Interfaces;

namespace RaceDay.Core.Entities;

public class LapEntity : IEntity
{
    public Guid Guid { get; init; }
    public Guid RaceDayGuid { get; init; }
    public float LapDistanceKm { get; set; }
    public float PetrolCostPerLap { get; set; }
}