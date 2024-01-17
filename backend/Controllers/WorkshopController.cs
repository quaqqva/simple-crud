using backend.Models;
using backend.Database;
using backend.Database.Repositories;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkshopController : BaseController<Workshop, WorkshopDTO>
    {
        protected override Repository<Workshop> Repository { get; init; }

        public WorkshopController(TypographyContext context)
        {
            Repository = new WorkshopRepository(context);
        }

        protected override Workshop EntityFromDTO(WorkshopDTO dto, int? id = null)
        {
            return new Workshop() {
                Id = id,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                ChiefId = dto.ChiefId ?? default
            };
        }
    }
}