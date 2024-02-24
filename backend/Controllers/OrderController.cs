using backend.Database;
using backend.Database.Repositories;
using backend.Dtos;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController(
        TypographyContext context,
        EntityNotificationHubOperator<Order> notificationHub
    ) : EntityController<Order, OrderDto>(notificationHub)
    {
        protected override Repository<Order> Repository { get; init; } =
            new OrderRepository(context);

        protected override Order EntityFromDto(OrderDto dto, int? id = null)
        {
            return new Order()
            {
                Id = id,
                ProductQuantity = dto.ProductQuantity,
                ProductId = dto.ProductId,
                ContractId = dto.ContractId
            };
        }
    }
}
