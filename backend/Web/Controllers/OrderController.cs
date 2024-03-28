using Backend.Application.Dtos;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(
    TypographyContext context,
    IEntityNotificationsService<Order> notificationsService
) : EntityController<Order, OrderDto>(notificationsService)
{
    protected override IRepository<Order> Repository { get; init; } =
        new OrderRepository(context);

    protected override Order EntityFromDto(OrderDto dto, Guid? id = null)
    {
        return new Order
        {
            Id = id,
            ProductQuantity = dto.ProductQuantity,
            ProductId = dto.ProductId,
            ContractId = dto.ContractId
        };
    }
}