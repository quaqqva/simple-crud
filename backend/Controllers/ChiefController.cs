using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChiefController : BaseController<Chief, ChiefViewModel>
    {
        public ChiefController(TypographyContext context): base(context, (context) => context.Chiefs) {}

        protected override Chief EntityFromViewModel(ChiefViewModel viewModel, int? id = null)
        {
            return new Chief() {
                Id = id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Patronymic = viewModel.Patronymic
            };
        }
    }
}