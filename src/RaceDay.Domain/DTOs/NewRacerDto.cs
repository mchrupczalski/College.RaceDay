namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents a new Racer
/// </summary>
public record NewRacerDto
{
    #region Properties

    /// <summary>
    ///     Racers name
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Racers age
    /// </summary>
    public byte Age { get; init; }

    #endregion
}