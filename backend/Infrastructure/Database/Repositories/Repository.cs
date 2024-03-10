using System.Data.Common;
using System.Linq.Expressions;
using Backend.Application.Enums;
using Backend.Application.Interfaces;
using Backend.Domain.Common;
using Backend.Infrastructure.Exceptions;
using Backend.Infrastructure.Expressions;
using Backend.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.Infrastructure.Database.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
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
            entity.Id = Guid.NewGuid();
            EntityEntry<TEntity> newEntityEntry = await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return newEntityEntry.Entity;
        }
        catch (DbException)
        {
            throw new DbIntegrityException();
        }
    }

    public async Task Update(Guid id, TEntity incoming)
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

    public async Task<TEntity> Read(Guid id, bool withDetails = false)
    {
        TEntity? entity = await (withDetails ? EntitiesDetails : DbSet).FirstOrDefaultAsync(
            (entity) => entity.Id == id
        );
        if (entity == null)
            throw new DbNotFoundException();
        return entity;
    }

    public async Task<TEntity> Delete(Guid id)
    {
        TEntity entity = await Read(id);
        try
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            throw new DbIntegrityException();
        }
    }
}