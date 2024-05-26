
namespace backend.Repository;

public interface ICrudRepository<T> 
{
    public T Create(T data);
    public T? GetById(long id);
    public T Update(T update);
    public T? Delete(long id);
    public IReadOnlyList<T> GetAll();
}