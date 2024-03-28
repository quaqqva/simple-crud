using System.Data.Common;
using Backend.Application.Enums;
using Backend.Application.Interfaces;
using Backend.Domain.Common;
using Backend.Infrastructure.Exceptions;
using Backend.Infrastructure.Expressions;
using Backend.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Database.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected TypographyContext _context;

    public Repository(TypographyContext context)
    {
        _context = context;
    }

    protected abstract DbSet<TEntity> DbSet { get; init; }

    protected abstract IQueryable<TEntity> EntitiesDetails { get; }

    public Task<int> Count => DbSet.CountAsync();

    public async Task<TEntity> Create(TEntity entity)
    {
        try
        {
            entity.Id = Guid.NewGuid();
            var newEntityEntry = await DbSet.AddAsync(entity);
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
            var source = await Read(id);
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
        var filterExpression =
            FilterExpressionGenerator<TEntity>.GenerateFilterExpression(filters);
        var entities = DbSet.AsNoTracking().Where(filterExpression);

        sortCriterias ??= ["id"];
        order ??= SortOrder.Ascending;
        var firstCriteriaExpression =
            SelectExpressionGenerator<TEntity>.GenerateSelectExpression<dynamic?>(sortCriterias[0]);
        entities = entities.OrderBy(firstCriteriaExpression, order);
        foreach (var criteria in sortCriterias.Skip(1))
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
        var entity = await (withDetails ? EntitiesDetails : DbSet).FirstOrDefaultAsync(
            entity => entity.Id == id
        );
        if (entity == null)
            throw new DbNotFoundException();
        return entity;
    }

    public async Task<TEntity> Delete(Guid id)
    {
        var entity = await Read(id);
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