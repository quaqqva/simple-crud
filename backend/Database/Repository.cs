using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public abstract class Repository<T> where T:class {
  protected TypographyContext _context;

  protected DbSet<T> _dbset;

  public Repository(TypographyContext context, Func<TypographyContext, DbSet<T>> dbSetSelector) {
    _context = context;
    _dbset = dbSetSelector(context);
  }
  public async void Create(T entity) {
    await _dbset.AddAsync(entity);
    await _context.SaveChangesAsync();
  }

  public async void Update(T entity) {
    _dbset.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async Task<T[]> Read() {
    var entities = await _dbset.ToArrayAsync();
    return entities;
  }

  public async Task<T> Read(int id) {
    var entity = await _dbset.FindAsync(id);
    if (entity == null) throw new Exception("No entity with such id");
    return entity;
  }

  public async void Delete(int id) {
    var entity = await Read(id);
    _dbset.Remove(entity);
    await _context.SaveChangesAsync();
  }
}