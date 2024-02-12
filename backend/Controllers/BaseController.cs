using System.Net;
using backend.Controllers.ErrorHandling;
using backend.Database;
using backend.Database.Exceptions;
using backend.Entities;
using backend.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public abstract class BaseController<TEntity, TDto>: ControllerBase where TEntity:class, IIdentifiable
    {
        protected abstract Repository<TEntity> Repository { get; init; }

        protected abstract TEntity EntityFromDTO(TDto dto, int? id = null);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get
        (
        int? limit,
        int? offset,
        [FromQuery] string[] sortBy,
        [FromQuery] string[] fields,
        [FromQuery] string filter = "",
        [FromQuery] string order = "ASC"
        )
        {
            if (limit != null || offset != null) Response.Headers["X-Total-Count"] = (await Repository.Count).ToString();
            string[]? sortCriterias = sortBy.Length == 0 ? null 
                                      : sortBy.SelectMany((fieldsThroughCommas) => fieldsThroughCommas.Split(',')).ToArray();
            string[]? parsedFields = fields.Length == 0 ? null
                                     : fields.SelectMany((fieldsThroughCommas) => fieldsThroughCommas.Split(',')).ToArray();
            try {
                var sortOrder = SortOrder.FromValue(order);
                var entities = await Repository.Read(limit, offset, sortCriterias, sortOrder, parsedFields, filter);
                return new ObjectResult(entities);
            } catch(ArgumentException e) {
                return new BadRequestObjectResult(new ErrorResult() {
                    Status = HttpStatusCode.BadRequest,
                    Errors = new() {
                        ["Invalid parameter"] = e.Message
                    }
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TEntity>> Get(int id) {
            try {
                return new ObjectResult(await Repository.Read(id, true));
            } catch (DbNotFoundException e) {
                return NotFoundObject(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post([FromBody] TDto dto) {
            try {
                var entity = EntityFromDTO(dto);
                return new ObjectResult(await Repository.Create(entity));
            } catch (DbIntegrityException e) {
                return IntegrityErrorObject(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(int id, [FromBody] TDto dto) {
            var entity = EntityFromDTO(dto);
            try {
                await Repository.Update(id, entity);
                return new ObjectResult(await Repository.Read(id));
            } catch(DbIntegrityException e) {
                return IntegrityErrorObject(e);
            } catch (DbNotFoundException e) {
                return NotFoundObject(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id) {
            try {
                await Repository.Delete(id);
                return Ok();
            } catch (DbIntegrityException e) {
                return IntegrityErrorObject(e);
            } catch (DbNotFoundException e) {
                return NotFoundObject(e);
            }
        }

        private ActionResult IntegrityErrorObject(Exception e) {
            var result = new ErrorResult() {
                Status = HttpStatusCode.UnprocessableEntity,
                Errors = new() {
                    ["IntegrityError"] = e.Message
                }
            };
            return new UnprocessableEntityObjectResult(result);
        }

        private ActionResult NotFoundObject(Exception e) {
            var result = new ErrorResult() {
                Status = HttpStatusCode.NotFound,
                Errors = new() {
                    ["NotFound"] = e.Message
                }
              };
            return new NotFoundObjectResult(result);
        }
    }
}