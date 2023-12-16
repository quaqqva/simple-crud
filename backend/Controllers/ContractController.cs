using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : BaseController<Contract, ContractViewModel>
    {
        public ContractController(TypographyContext context): base(context, (context) => context.Contracts) {}

        protected override Contract EntityFromViewModel(ContractViewModel viewModel, int? id = null)
        {
            return new Contract() {
                Number = id,
                CompletionDate = viewModel.CompletionDate,
                RegistrationDate = viewModel.RegistrationDate,
                CustomerId = viewModel.CustomerId
            };
        }
    }
}