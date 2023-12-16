namespace backend.Repositories;

public abstract class Repository<T> {
  public abstract void Create(T entity);

  public abstract void Update(int id, T entity);

  public abstract T[] Read();

  public abstract T Read(int id);

  public abstract void Delete(int id);
}