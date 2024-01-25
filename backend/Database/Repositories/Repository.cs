using System.Data.Common;
using System.Linq.Expressions;
using backend.Database.Exceptions;
using backend.Entities;
using backend.Utilities;
using backend.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Database;

public abstract class Repository<T> where T:class, IIdentifiable {
  protected TypographyContext _context;

  protected abstract DbSet<T> DbSet { get; init; }

  protected abstract IQueryable<T> EntitiesDetails { get; }

  protected abstract Dictionary<string, Expression<Func<T, dynamic?>>> PropertyCallbacks { get; }

  public Task<int> Count { get => DbSet.CountAsync(); }

  public Repository(TypographyContext context) {
    _context = context;
  }

  public async Task<T> Create(T entity) {
    try {
      EntityEntry<T> newEntityEntry = await DbSet.AddAsync(entity);
      await _context.SaveChangesAsync();
      return newEntityEntry.Entity;
    } catch (DbException) {
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

  public async Task<IEnumerable<T>> Read(int? limit, int? offset, string[]? sortCriterias, SortOrder? order) {
    IQueryable<T> entities = DbSet.AsNoTracking();

    sortCriterias ??= ["id"];
    order ??= SortOrder.Ascending;

    var firstCriteriaExpression = GetPropertyExpression(sortCriterias[0]);
    entities = entities.OrderBy(firstCriteriaExpression, order);

    foreach(string criteria in sortCriterias.Skip(1)) {
      var propertyExpression = GetPropertyExpression(criteria);
      entities = ((IOrderedQueryable<T>)entities).ThenBy(propertyExpression, order);
    }

    if (offset != null) entities = entities.Skip(offset.Value);
    if (limit != null) entities = entities.Take(limit.Value);
    return await entities.ToArrayAsync();
  }

  public async Task<T> Read(int id, bool withDetails = false) {
    T? entity = await (withDetails ? EntitiesDetails : DbSet).FirstOrDefaultAsync((entity) => entity.Id == id);
    if (entity == null) throw new DbNotFoundException();
    return entity;
  }

  public async Task Delete(int id) {
    T entity = await Read(id);
    try {
      DbSet.Remove(entity);
      await _context.SaveChangesAsync();
    } catch {
      throw new DbIntegrityException();
    }
  }

  /// <summary>
  /// Property callback resolver for LINQ queries
  /// Throws an exception if property not found
  /// </summary>
  /// <param name="name">Property name in camel case</param>
  /// <returns>Property callback</returns>
  private Expression<Func<T, dynamic?>> GetPropertyExpression(string name) {
    if (name == "id") return (T entity) => entity.Id;
    try {
      var callback = PropertyCallbacks[name];
      return callback;
    } catch(KeyNotFoundException) {
      throw new ArgumentException($"No such property in entity: '{name}'");
    }
  }
}