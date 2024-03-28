using Backend.Application.Dtos;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Database;
using Backend.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("customers")]
public class CustomerController(
    TypographyContext context,
    IEntityNotificationsService<Customer> notificationsService
) : EntityController<Customer, CustomerDto>(notificationsService)
{
    protected override IRepository<Customer> Repository { get; init; } =
        new CustomerRepository(context);

    protected override Customer EntityFromDto(CustomerDto dto, Guid? id = null)
    {
        return new Customer
        {
            Id = id,
            Name = dto.Name,
            AddressId = dto.AddressId
        };
    }
}