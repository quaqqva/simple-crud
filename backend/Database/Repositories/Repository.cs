using System.Data.Common;
using backend.Database.Exceptions;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public abstract class Repository<T> where T:class, IIdentifiable {
  protected TypographyContext _context;

  protected abstract DbSet<T> DbSet { get; init; }

  protected abstract IQueryable<T> EntitiesDetails { get; }

  public Task<int> Count { get => DbSet.CountAsync(); }

  public Repository(TypographyContext context) {
    _context = context;
  }

  public async Task<T> Create(T entity) {
    try {
      var newEntity = await DbSet.AddAsync(entity);
      await _context.SaveChangesAsync();
      return newEntity.Entity;
    } catch {
      throw new DbIntegrityException();
    }
  }

  public async Task Update(int id, T incoming) {
    try {
      T source = await Read(id);
      incoming.Id = id;
      _context.Entry(source).CurrentValues.SetValues(incoming);
      await _context.SaveChangesAsync();
    }
    catch (DbException) {
      throw new DbIntegrityException();
    }
  }

  public async Task<T[]> Read(int? limit, int? offset) {
    var entities = DbSet as IQueryable<T>;
    if (offset != null) entities = entities.Skip(offset.Value);
    if (limit != null) entities = entities.Take(limit.Value);
    return await entities.ToArrayAsync();
  }

  public async Task<T> Read(int id, bool withDetails = false) {
    var entity = await (withDetails ? EntitiesDetails : DbSet).FirstOrDefaultAsync((entity) => entity.Id == id);
    if (entity == null) throw new DbNotFoundException();
    return entity;
  }

  public async Task Delete(int id) {
    var entity = await Read(id);
    if (entity == null) throw new DbNotFoundException();
    try {
      DbSet.Remove(entity);
      await _context.SaveChangesAsync();
    } catch {
      throw new DbIntegrityException();
    }
  }
}