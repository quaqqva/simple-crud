using backend.Models;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController<Product, ProductViewModel>
    {
        public ProductController(TypographyContext context): 
        base(context, 
            (context) => context.Products,
            (product) => product.Code,
            (context) => context.Products
                                .Include((product) => product.Orders)
                                .Include((product) => product.Workshop)
                                .ToArrayAsync()) {}

        protected override Product EntityFromViewModel(ProductViewModel viewModel, int? id = null)
        {
            return new Product() {
                Code = id,
                Name = viewModel.Name,
                WorkshopNumber = viewModel.WorkshopNumber,
            };
        }
    }
}