using backend.Entities;
using backend.Database;
using backend.Database.Repositories;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController<Order, OrderDTO>
    {
        protected override Repository<Order> Repository { get; init; }

        public OrderController(TypographyContext context)
        {
            Repository = new OrderRepository(context);
        }

        protected override Order EntityFromDTO(OrderDTO dto, int? id = null)
        {
            return new Order() {
                Id = id,
                ProductQuantity = dto.ProductQuantity,
                ProductCode = dto.ProductCode,
                ContractNumber = dto.ContractNumber
            };
        }
    }
}