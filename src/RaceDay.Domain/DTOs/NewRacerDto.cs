namespace RaceDay.Domain.DTOs;

public record NewRacerDto
{
    #region Properties

    public string? Name { get; init; }
    public byte Age { get; init; }

    #endregion
}