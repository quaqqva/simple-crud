using backend.Models;
using backend.Database;
using backend.Database.Repositories;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : BaseController<Contract, ContractDTO>
    {
        protected override Repository<Contract> Repository { get; init; }
        public ContractController(TypographyContext context) {
            Repository = new ContractRepository(context);
        }


        protected override Contract EntityFromDTO(ContractDTO dto, int? id = null)
        {
            return new Contract() {
                Id = id,
                CompletionDate = dto.CompletionDate ?? default,
                RegistrationDate = dto.RegistrationDate,
                CustomerId = dto.CustomerId ?? default
            };
        }
    }
}