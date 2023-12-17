using backend.Database.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class Repository<T> where T:class {
  protected TypographyContext _context;

  protected DbSet<T> _dbset;

  public Repository(TypographyContext context, Func<TypographyContext, DbSet<T>> dbSetSelector) {
    _context = context;
    _dbset = dbSetSelector(context);
  }
  public async Task<T> Create(T entity) {
    var newEntity = await _dbset.AddAsync(entity);
    await _context.SaveChangesAsync();

    return newEntity.Entity;
  }

  public async Task Update(T entity) {
    try {
      _dbset.Update(entity);
      await _context.SaveChangesAsync();
    } catch {
      throw new DbIntegrityException();
    }
  }

  public async Task<T[]> Read() {
    var entities = await _dbset.ToArrayAsync();
    return entities;
  }

  public async Task<T> Read(int id) {
    var entity = await _dbset.FindAsync(id);
    if (entity == null) throw new DbNotFoundException();
    return entity;
  }

  public async Task Delete(int id) {
    var entity = await Read(id);
    if (entity == null) throw new DbNotFoundException();
    try {
      _dbset.Remove(entity);
      await _context.SaveChangesAsync();
    } catch {
      throw new DbIntegrityException();
    }

  }
}