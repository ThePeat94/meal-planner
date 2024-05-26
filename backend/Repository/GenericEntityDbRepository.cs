using backend.Entity;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

class GenericEntityDbRepository<T> : ICrudRepository<T> where T: class
{
    private DbSet<T> m_set;
    private DbContext m_context;
    
    public GenericEntityDbRepository(DbSet<T> set, DbContext context)
    {
        this.m_set = set;
    }

    public T Create(T data)
    {
        this.m_set.Add(data);
        this.m_context.SaveChanges();
        throw new NotImplementedException();
    }

    public T? GetById(long id)
    {
        throw new NotImplementedException();
    }

    public T Update(T update)
    {
        throw new NotImplementedException();
    }

    public T? Delete(long id)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<T> GetAll()
    {
        throw new NotImplementedException();
    }
}