namespace RaceDay.Domain.Entities;

/// <summary>
///     The racer entity.
/// </summary>
public record RacerEntity : EntityBase
{
    #region Properties

    /// <summary>
    ///     The name of the racer.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     The age of the racer.
    /// </summary>
    public byte Age { get; init; }

    #endregion
}