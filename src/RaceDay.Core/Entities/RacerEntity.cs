namespace RaceDay.Core.Entities;

public class RacerEntity
{
    public Guid Guid { get; init; }
    public string Name { get; set; }
    public sbyte Age { get; set; }
}