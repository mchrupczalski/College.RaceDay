namespace RaceDay.Domain.DTOs;

public record DaySummaryDto
{
    public int RaceDayId { get; init; }
    public string? RaceDayName { get; init; }
    public float SignUpFee { get; init; }
    public float LapDistanceKm { get; init; }
    public float PetrolCostPerLap { get; init; }
    public int TotalRaces { get; init; }
    public float RecordLapTime { get; init; }
    public string? RecordHolderName { get; init; }
    public float TotalIncome { get; init; }
    public float TotalCost { get; init; }
}