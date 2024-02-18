using System.Data.Common;
using System.Linq.Expressions;
using backend.Database.Exceptions;
using backend.Entities;
using backend.Utilities;
using backend.Utilities.Expressions;
using backend.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Database;

public abstract class Repository<TEntity>
    where TEntity : class, IIdentifiable
{
    protected TypographyContext _context;

    protected abstract DbSet<TEntity> DbSet { get; init; }

    protected abstract IQueryable<TEntity> EntitiesDetails { get; }

    public Task<int> Count
    {
        get => DbSet.CountAsync();
    }

    public Repository(TypographyContext context)
    {
        _context = context;
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        try
        {
            EntityEntry<TEntity> newEntityEntry = await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return newEntityEntry.Entity;
        }
        catch (DbException)
        {
            throw new DbIntegrityException();
        }
    }

    public async Task Update(int id, TEntity incoming)
    {
        try
        {
            TEntity source = await Read(id);
            incoming.Id = id;
            _context.Entry(source).CurrentValues.SetValues(incoming);
            await _context.SaveChangesAsync();
        }
        catch (DbException)
        {
            throw new DbIntegrityException();
        }
    }

    public Task<TEntity[]> Read(
        int? limit,
        int? offset,
        string[]? sortCriterias,
        SortOrder? order,
        string[]? fields,
        string filters
    )
    {
        Expression<Func<TEntity, bool>> filterExpression =
            FilterExpressionGenerator<TEntity>.GenerateFilterExpression(filters);
        IQueryable<TEntity> entities = DbSet.AsNoTracking().Where(filterExpression);

        sortCriterias ??= ["id"];
        order ??= SortOrder.Ascending;
        var firstCriteriaExpression =
            SelectExpressionGenerator<TEntity>.GenerateSelectExpression<dynamic?>(sortCriterias[0]);
        entities = entities.OrderBy(firstCriteriaExpression, order);
        foreach (string criteria in sortCriterias.Skip(1))
        {
            var propertyExpression =
                SelectExpressionGenerator<TEntity>.GenerateSelectExpression<dynamic?>(criteria);
            entities = ((IOrderedQueryable<TEntity>)entities).ThenBy(propertyExpression, order);
        }

        if (fields != null)
            entities = entities.Select(
                SelectExpressionGenerator<TEntity>.GenerateSelectExpression(fields)
            );

        if (offset != null)
            entities = entities.Skip(offset.Value);
        if (limit != null)
            entities = entities.Take(limit.Value);
        return entities.ToArrayAsync();
    }

    public async Task<TEntity> Read(int id, bool withDetails = false)
    {
        TEntity? entity = await (withDetails ? EntitiesDetails : DbSet).FirstOrDefaultAsync(
            (entity) => entity.Id == id
        );
        if (entity == null)
            throw new DbNotFoundException();
        return entity;
    }

    public async Task Delete(int id)
    {
        TEntity entity = await Read(id);
        try
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new DbIntegrityException();
        }
    }
}
