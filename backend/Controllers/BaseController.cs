using System.Net;
using backend.Controllers.ErrorHandling;
using backend.Database;
using backend.Database.Exceptions;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public abstract class BaseController<T, V>: ControllerBase where T:class, IIdentifiable
    {
        protected abstract Repository<T> Repository { get; init; }

        protected abstract T EntityFromDTO(V dto, int? id = null);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get() {
            return await Repository.Read();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<T>> Get(int id) {
            try {
                return new ObjectResult(await Repository.Read(id, true));
            } catch (DbNotFoundException e) {
                return NotFoundObject(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<T>> Post([FromBody] V dto) {
            try {
                var entity = EntityFromDTO(dto);
                return new ObjectResult(await Repository.Create(entity));
            } catch (DbIntegrityException e) {
                return IntegrityErrorObject(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<T>> Put(int id, [FromBody] V dto) {
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
        public async Task<ActionResult<T>> Delete(int id) {
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
                Errors = new Dictionary<string, IEnumerable<string>>() {
                    { "IntegrityError", new string[] { e.Message } }
                }
            };
            return new UnprocessableEntityObjectResult(result);
        }

        private ActionResult NotFoundObject(Exception e) {
            var result = new ErrorResult() {
                Status = HttpStatusCode.NotFound,
                Errors = new Dictionary<string, IEnumerable<string>>() {
                    { "NotFound", new string[] { e.Message } }
                }
              };
            return new NotFoundObjectResult(result);
        }
    }
}