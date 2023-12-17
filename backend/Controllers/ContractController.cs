using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : BaseController<Contract, ContractViewModel>
    {
        public ContractController(TypographyContext context): 
        base(
            context, 
            (context) => context.Contracts,
            (contract) => contract.Number,
            (context) => context.Contracts
                                .Include(contract => contract.Customer)
                                .Include(customer => customer.Orders)
                                .ToArrayAsync()
        ) {}

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