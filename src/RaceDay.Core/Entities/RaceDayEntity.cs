namespace RaceDay.Core.Entities;

public class RaceDayEntity
{
    public Guid Guid { get; init; }
    public string Name { get; set; }
    public float SignUpFee { get; init; }
}