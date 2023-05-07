namespace RaceDay.Domain.DTOs;

public record RacerDto
{
    public int RacerId { get; init; }
    public string? RacerName { get; init; }
    public byte Age { get; init; }
}