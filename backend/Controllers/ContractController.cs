using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("contracts")]
    public class ContractController : BaseController<Contract, ContractDto>
    {
        protected override Repository<Contract> Repository { get; init; }

        public ContractController(TypographyContext context)
        {
            Repository = new ContractRepository(context);
        }

        protected override Contract EntityFromDto(ContractDto dto, int? id = null)
        {
            return new Contract()
            {
                Id = id,
                CompletionDate = dto.CompletionDate,
                RegistrationDate = dto.RegistrationDate,
                CustomerId = dto.CustomerId
            };
        }
    }
}
