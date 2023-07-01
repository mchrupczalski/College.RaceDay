namespace RaceDay.Domain.Interfaces;

public interface IExchangeDataCommand<T>
{

    /// <summary>
    ///     Creates or Updates an entity and returns the new record
    /// </summary>
    /// <param name="entity">Record to create or update</param>
    /// <returns>The created or updated entity</returns>
    T? Execute(T entity);
}