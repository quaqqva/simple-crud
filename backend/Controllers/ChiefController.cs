using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("chiefs")]
    public class ChiefController(
        TypographyContext context,
        EntityNotificationHubOperator<Chief> notificationHub
    ) : EntityController<Chief, ChiefDto>(notificationHub)
    {
        protected override Repository<Chief> Repository { get; init; } =
            new ChiefRepository(context);

        protected override Chief EntityFromDto(ChiefDto dto, int? id = null)
        {
            return new Chief()
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Patronymic = dto.Patronymic
            };
        }
    }
}
