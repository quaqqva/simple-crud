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
              return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<T>> Post([FromBody] V dto) {
            if (dto == null) return new BadRequestResult();
            try {
                var entity = EntityFromDTO(dto);
                return new ObjectResult(await Repository.Create(entity));
            } catch (Exception e) {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<T>> Put(int id, [FromBody] V dto) {
            if (dto == null) return new BadRequestResult();

            var entity = EntityFromDTO(dto);
            try {
                await Repository.Update(id, entity);
                return new ObjectResult(await Repository.Read(id));
            } catch(DbIntegrityException e) {
                return new BadRequestObjectResult(e.Message);
            } catch (DbNotFoundException e) {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> Delete(int id) {
            try {
                await Repository.Delete(id);
                return Ok();
            } catch (DbIntegrityException e) {
                return new BadRequestObjectResult(e.Message);
            } catch (DbNotFoundException e) {
                return NotFound(e.Message);
            }
        }
    }
}