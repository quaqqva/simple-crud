using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkshopController : BaseController<Workshop, WorkshopViewModel>
    {
        public WorkshopController(TypographyContext context): base(context, (context) => context.Workshops) {}

        protected override Workshop EntityFromViewModel(WorkshopViewModel viewModel, int? id = null)
        {
            return new Workshop() {
                Number = id,
                Name = viewModel.Name,
                PhoneNumber = viewModel.PhoneNumber,
                ChiefId = viewModel.ChiefId
            };
        }
    }
}