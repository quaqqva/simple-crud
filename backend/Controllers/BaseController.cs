using System.Net;
using backend.Controllers.ErrorHandling;
using backend.Database;
using backend.Database.Exceptions;
using backend.Entities;
using backend.Utilities.Enums;
using backend.WebSocket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    // [Authorize]
    public abstract class EntityController<TEntity, TDto>(
        EntityNotificationHubOperator<TEntity> notificationHub
    ) : ControllerBase
        where TEntity : class, IIdentifiable
    {
        private readonly EntityNotificationHubOperator<TEntity> _notificationHub = notificationHub;

        protected abstract Repository<TEntity> Repository { get; init; }

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
            try
            {
                string[]? sortCriterias =
                    sortBy.Length == 0
                        ? null
                        : sortBy
                            .SelectMany((fieldsThroughCommas) => fieldsThroughCommas.Split(','))
                            .ToArray();
                string[]? parsedFields =
                    fields.Length == 0
                        ? null
                        : fields
                            .SelectMany((fieldsThroughCommas) => fieldsThroughCommas.Split(','))
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
            catch (NullReferenceException)
            {
                return new BadRequestObjectResult(
                    new ErrorResult()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Errors = new() { ["Invalid parameter"] = "Query parameter can't be empty" }
                    }
                );
            }
            catch (ArgumentException e)
            {
                return new BadRequestObjectResult(
                    new ErrorResult()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Errors = new() { ["Invalid parameter"] = e.Message }
                    }
                );
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TEntity>> Get(Guid id)
        {
            try
            {
                return new ObjectResult(await Repository.Read(id, true));
            }
            catch (DbNotFoundException e)
            {
                return EntityController<TEntity, TDto>.NotFoundObject(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post([FromBody] TDto dto)
        {
            try
            {
                var entity = EntityFromDto(dto);
                var createdEntity = await Repository.Create(entity);
                await _notificationHub.Broadcast(EntityActionType.Created, entity);
                return new ObjectResult(createdEntity);
            }
            catch (DbIntegrityException e)
            {
                return EntityController<TEntity, TDto>.IntegrityErrorObject(e);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TEntity>> Put(Guid id, [FromBody] TDto dto)
        {
            var entity = EntityFromDto(dto);
            try
            {
                await Repository.Update(id, entity);
                await _notificationHub.Broadcast(EntityActionType.Updated, entity);
                return new ObjectResult(await Repository.Read(id));
            }
            catch (DbIntegrityException e)
            {
                return EntityController<TEntity, TDto>.IntegrityErrorObject(e);
            }
            catch (DbNotFoundException e)
            {
                return EntityController<TEntity, TDto>.NotFoundObject(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(Guid id)
        {
            try
            {
                var deletedEntity = await Repository.Delete(id);
                await _notificationHub.Broadcast(EntityActionType.Deleted, deletedEntity);
                return Ok();
            }
            catch (DbIntegrityException e)
            {
                return EntityController<TEntity, TDto>.IntegrityErrorObject(e);
            }
            catch (DbNotFoundException e)
            {
                return EntityController<TEntity, TDto>.NotFoundObject(e);
            }
        }

        private static UnprocessableEntityObjectResult IntegrityErrorObject(Exception e)
        {
            var result = new ErrorResult()
            {
                Status = HttpStatusCode.UnprocessableEntity,
                Errors = new() { ["IntegrityError"] = e.Message }
            };
            return new UnprocessableEntityObjectResult(result);
        }

        private static NotFoundObjectResult NotFoundObject(Exception e)
        {
            var result = new ErrorResult()
            {
                Status = HttpStatusCode.NotFound,
                Errors = new() { ["NotFound"] = e.Message }
            };
            return new NotFoundObjectResult(result);
        }
    }
}
