namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A command for creating or updating an entity.
/// </summary>
/// <typeparam name="T">The type of entity to create or update</typeparam>
public interface IExchangeDataCommand<T>
{
    #region Abstract Members

    /// <summary>
    ///     Creates or Updates an entity and returns the new record
    /// </summary>
    /// <param name="entity">Record to create or update</param>
    /// <returns>The created or updated entity</returns>
    T? Execute(T entity);

    #endregion
}