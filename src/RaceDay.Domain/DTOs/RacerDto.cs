namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents a Racer
/// </summary>
public record RacerDto
{
    #region Properties

    /// <summary>
    ///     A unique identifier for the Racer
    /// </summary>
    public int Id { get; init; }

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