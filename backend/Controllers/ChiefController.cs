using backend.Models;
using backend.Database;
using backend.Database.Repositories;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChiefController : BaseController<Chief, ChiefDTO>
    {
        protected override Repository<Chief> Repository { get; init; }

        public ChiefController(TypographyContext context) 
        {
            Repository = new ChiefRepository(context);
        }

        protected override Chief EntityFromDTO(ChiefDTO dto, int? id = null)
        {
            return new Chief() {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Patronymic = dto.Patronymic
            };
        }
    }
}