using Backend.Database;
using Backend.Entity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

class GenericEntityDbRepository<T> : ICrudRepository<T> where T: class, IEntity
{
    protected readonly DbSet<T> m_set;
    protected readonly MealPlannerContext m_context;
    
    public GenericEntityDbRepository(MealPlannerContext context, DbSet<T> set)
    {
        this.m_set = set;
        this.m_context = context;
    }

    public virtual T Create(T data)
    {
        var entry = this.m_set.Add(data);
        this.m_context.SaveChanges();
        return entry.Entity;
    }

    public virtual T? GetById(long id)
    {
        return this.m_set.Find(id);
    }

    public virtual T Update(T update)
    {
        var tracked = this.m_set.Find(update.Id);
        if (tracked is null)
        {
            throw new InvalidOperationException("Tried to update non existing entity");
        }

        var entry = this.m_context.Entry(tracked);
        entry.CurrentValues.SetValues(update);
        this.m_context.SaveChanges();
        return entry.Entity;
    }

    public virtual T? Delete(long id)
    {
        var found = this.GetById(id);
        if (found is null)
        {
            return null;
        }
        
        this.m_set.Remove(found);
        this.m_context.SaveChanges();

        return found;
    }

    public virtual IReadOnlyList<T> GetAll()
    {
        return this.m_set.ToList();
    }
}