namespace RaceDay.Domain.DTOs;

public record RaceDto
{
    public int RaceDayId { get; init; }
    public string? RaceDayName { get; init; }
    public int RaceNumber { get; init; }
    public DateTime RaceDate { get; init; }
    public float SignUpFee { get; init; }
    public float AllTimeLapRecordSeconds { get; init; }
    public IEnumerable<RacerDto>? Racers { get; init; }
    public float RaceLapRecordSeconds { get; init; }
    public bool IsRecordBeaten { get; init; }
}