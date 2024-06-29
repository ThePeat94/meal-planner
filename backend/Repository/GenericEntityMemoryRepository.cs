using System.Collections.Concurrent;
using Backend.Entity;

namespace Backend.Repository;

public class GenericEntityMemoryRepository<T> : ICrudRepository<T> where T : IEntity
{
    private readonly object m_creationLock = new();
    private ConcurrentDictionary<long, T> m_data = new();
    private int m_latestId;
    
    public T Create(T data)
    {
        T created;
        lock (this.m_creationLock)
        {
            this.m_latestId++;
            data.Id = this.m_latestId;
            created = this.m_data.AddOrUpdate(this.m_latestId, (id) => data, (id, entity) => data);
        }

        return created;
    }

    public T? GetById(long id)
    {
         this.m_data.TryGetValue(id, out var value);
         return value;
    }

    public T Update(T update)
    {
        var updated = this.m_data.AddOrUpdate(update.Id, (id) => update, (id, entity) => update);
        return updated;
    }

    public T? Delete(long id)
    {
        this.m_data.TryRemove(id, out var removed);
        return removed;
    }

    public IReadOnlyList<T> GetAll()
    {
        return this.m_data.Values.ToList();
    }
}