using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController<Order, OrderViewModel>
    {
        public OrderController(TypographyContext context): 
        base(
            context, 
            (context) => context.Orders,
            (order) => order.Id,
            (context) => context.Orders
                                .Include((order) => order.Contract)
                                .Include((order) => order.Product)
                                .ToArrayAsync()
        ) {}

        protected override Order EntityFromViewModel(OrderViewModel viewModel, int? id = null)
        {
            return new Order() {
                Id = id,
                ProductQuantity = viewModel.ProductQuantity,
                ProductCode = viewModel.ProductCode,
                ContractNumber = viewModel.ContractNumber
            };
        }

        protected override Order UpdateEntity(Order source, Order incoming)
        {
            source.ProductCode = incoming.ProductCode;
            source.ProductQuantity = incoming.ProductQuantity;
            source.ContractNumber = incoming.ContractNumber;
            return source;
        }
    }
}