using backend.Database;
using backend.Database.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    public abstract class BaseController<T, V>: ControllerBase where T:class
    {
        protected Repository<T> _repository;

        public BaseController(TypographyContext context, Func<TypographyContext, DbSet<T>> selector) {
            _repository = new Repository<T>(context, selector);
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
        public abstract Task<ActionResult<T>> Post([FromBody] V viewModel);

        [HttpPut("{id}")]
        public abstract Task<ActionResult<T>> Put(int id, [FromBody] V viewModel);

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
    }
}