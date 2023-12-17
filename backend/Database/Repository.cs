using backend.Database.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class Repository<T> where T:class {
  protected TypographyContext _context;

  protected DbSet<T> _dbset;

  protected Func<TypographyContext, Task<T[]>> _fullEntitiesSelector;

  protected Func<T, int?> _idSelecor;

  public Repository(TypographyContext context, 
  Func<TypographyContext, DbSet<T>> dbSetSelector,
  Func<T, int?> idSelector,
  Func<TypographyContext, Task<T[]>> fullEntitiesSelector) {
    _context = context;
    _dbset = dbSetSelector(context);
    _idSelecor = idSelector;
    _fullEntitiesSelector = fullEntitiesSelector;
  }
  public async Task<T> Create(T entity) {
    try {
      var newEntity = await _dbset.AddAsync(entity);
      await _context.SaveChangesAsync();
  
      return newEntity.Entity;
    } catch {
      throw new DbIntegrityException();
    }
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
    var entities = await _fullEntitiesSelector(_context);
    return entities;
  }

  public async Task<T> Read(int id) {
    var entity = (await _fullEntitiesSelector(_context)).FirstOrDefault((entity) => _idSelecor(entity) == id);
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