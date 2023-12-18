using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChiefController : BaseController<Chief, ChiefViewModel>
    {
        public ChiefController(TypographyContext context): 
        base(context, 
        (context) => context.Chiefs,
        (chief) => chief.Id,
        (context) => context.Chiefs.Include(chief => chief.Workshops).ToArrayAsync()) {}

        protected override Chief EntityFromViewModel(ChiefViewModel viewModel, int? id = null)
        {
            return new Chief() {
                Id = id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Patronymic = viewModel.Patronymic
            };
        }

        protected override Chief UpdateEntity(Chief source, Chief incoming)
        {
            source.LastName = incoming.LastName;
            source.FirstName = incoming.FirstName;
            source.Patronymic = incoming.Patronymic;
            return source;
        }
    }
}