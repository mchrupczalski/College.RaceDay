using System.Linq.Expressions;
using Newtonsoft.Json;
using RaceDay.Core.Interfaces;
using RaceDay.Core.Providers;

namespace RaceDay.Core.Repositories;

public class FileRepository<T> : IRepository<T>
    where T : class, IEntity
{
    #region Fields

    private readonly string _filePath;
    private readonly IList<T> _entities = new List<T>();

    #endregion

    #region Constructors

    public FileRepository(string filePath) => _filePath = filePath;

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public void Create(T entity)
    {
        _entities.Add(entity);
        FileProvider.WriteFile(_filePath, JsonConvert.SerializeObject(_entities));
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        var target = _entities.FirstOrDefault(x => x.Guid == entity.Guid);
        if (target == null) return;
        
        _entities.Remove(target);
        _entities.Add(entity);
        FileProvider.WriteFile(_filePath, JsonConvert.SerializeObject(_entities));
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        var target = _entities.FirstOrDefault(x => x.Guid == entity.Guid);
        if (target == null) return;
        
        _entities.Remove(target);
        FileProvider.WriteFile(_filePath, JsonConvert.SerializeObject(_entities));
    }

    /// <inheritdoc />
    public Task<T> GetById(int id) => throw new NotImplementedException();

    /// <inheritdoc />
    public Task<IEnumerable<T>> GetAll()
    {
        string data = FileProvider.ReadFile(_filePath);
        _entities.Clear();
        
        var newItems = JsonConvert.DeserializeObject<List<T>>(data);
        if (newItems == null) return Task.FromResult(_entities.AsEnumerable());
        
        foreach (var item in newItems)
        {
            _entities.Add(item);
        }

        return Task.FromResult(_entities.AsEnumerable());
    }

    /// <inheritdoc />
    public Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> predicate) => throw new NotImplementedException();

    #endregion
}