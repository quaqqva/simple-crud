using Backend.Application.Enums;
using Backend.Application.Interfaces;
using Backend.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
public abstract class EntityController<TEntity, TDto>(
    IEntityNotificationsService<TEntity> notificationsService
) : ControllerBase where TEntity : BaseEntity
{
    private readonly IEntityNotificationsService<TEntity> _notificationsService = notificationsService;

    protected abstract IRepository<TEntity> Repository { get; init; }

    protected abstract TEntity EntityFromDto(TDto dto, Guid? id = null);

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TEntity>>> Get(
        int? limit,
        int? offset,
        [FromQuery] string[] sortBy,
        [FromQuery] string[] fields,
        [FromQuery] string filter = "",
        [FromQuery] string order = "ASC"
    )
    {
        if (limit != null || offset != null)
            Response.Headers["X-Total-Count"] = (await Repository.Count).ToString();
        var sortCriterias =
            sortBy.Length == 0
                ? null
                : sortBy
                    .SelectMany(fieldsThroughCommas => fieldsThroughCommas.Split(','))
                    .ToArray();
        var parsedFields =
            fields.Length == 0
                ? null
                : fields
                    .SelectMany(fieldsThroughCommas => fieldsThroughCommas.Split(','))
                    .ToArray();
        var sortOrder = SortOrder.FromValue(order);
        var entities = await Repository.Read(
            limit,
            offset,
            sortCriterias,
            sortOrder,
            parsedFields,
            filter
        );
        return new ObjectResult(entities);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TEntity>> Get(Guid id)
    {
        return new ObjectResult(await Repository.Read(id, true));
    }

    [HttpPost]
    public async Task<ActionResult<TEntity>> Post([FromBody] TDto dto)
    {
        var entity = EntityFromDto(dto);
        var createdEntity = await Repository.Create(entity);
        await _notificationsService.Broadcast(EntityActionType.Created, entity);
        return new ObjectResult(createdEntity);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TEntity>> Put(Guid id, [FromBody] TDto dto)
    {
        var entity = EntityFromDto(dto);
        await Repository.Update(id, entity);
        await _notificationsService.Broadcast(EntityActionType.Updated, entity);
        return new ObjectResult(await Repository.Read(id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TEntity>> Delete(Guid id)
    {
        var deletedEntity = await Repository.Delete(id);
        await _notificationsService.Broadcast(EntityActionType.Deleted, deletedEntity);
        return Ok();
    }
}