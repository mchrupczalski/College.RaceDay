namespace RaceDay.Domain.DTOs;

public record RacerDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public byte Age { get; init; }
}