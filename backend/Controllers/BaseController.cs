using backend.Database;
using backend.Database.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    public abstract class BaseController<T, V>: ControllerBase where T:class
    {
        protected Repository<T> _repository;

        public BaseController(TypographyContext context, 
        Func<TypographyContext, DbSet<T>> dbSetSelector,
        Func<T, int?> idSelector,
        Func<TypographyContext, Task<T[]>> fullEntitiesSelector) {
            _repository = new Repository<T>(context, dbSetSelector, idSelector, fullEntitiesSelector);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get() {
            return await _repository.Read();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int id) {
            try {
              return new ObjectResult(await _repository.Read(id));
            } catch (DbNotFoundException) {
              return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<T>> Post([FromBody] V viewModel) {
            if (viewModel == null) return new BadRequestResult();
            var entity = EntityFromViewModel(viewModel);
            return new ObjectResult(await _repository.Create(entity));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<T>> Put(int id, [FromBody] V viewModel) {
            if (viewModel == null) return new BadRequestResult();

            var entity = EntityFromViewModel(viewModel);
            try {
                await _repository.Update(entity);
                return new ObjectResult(await _repository.Read(id));
            } catch {
                return new BadRequestResult();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> Delete(int id) {
            try {
                await _repository.Delete(id);
                return Ok();
            } catch (DbIntegrityException e) {
                return new BadRequestObjectResult(e.Message);
            } catch (DbNotFoundException e) {
                return NotFound(e.Message);
            }
        }

        protected abstract T EntityFromViewModel(V viewModel, int? id = null);
    }
}