namespace RaceDay.Domain.DTOs;

public record RaceDaySummaryDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public float SignUpFee { get; init; }
    public float LapDistanceKilometers { get; init; }
    public float PetrolCostPerLap { get; init; }
    public int TotalRaces { get; init; }
    public TimeSpan? RecordLap { get; init; }
    public string? RecordHolderName { get; init; }
    public float AverageProfit { get; init; }
}