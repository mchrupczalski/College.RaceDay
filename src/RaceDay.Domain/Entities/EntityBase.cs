namespace RaceDay.Domain.Entities;

/// <summary>
///     The base class for all entities with a unique identifier.
/// </summary>
public abstract record EntityBase
{
    /// <summary>
    ///     The unique identifier for the entity.
    /// </summary>
    public int Id { get; init; }
}