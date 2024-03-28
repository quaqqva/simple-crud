using Backend.Domain.Entities;
using Backend.Infrastructure.WebSockets;
using Backend.Web.ErrorHandling;

namespace Backend.Web.DependencyInjection;

public static class ApplicationExtension
{
    public static void AddConfiguration(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

        application.UseMiddleware<ExceptionMiddleware>();
        application.UseHttpsRedirection();
        application.UseAuthentication();
        application.UseAuthorization();

        application.UseCors(ServiceCollectionExtensions.CorsPolicyName);

        application.MabWebsocketHubs();
        application.MapControllers();
        application.MapDefaultControllerRoute();
    }

    private static void MabWebsocketHubs(this WebApplication application)
    {
        application.MapHub<EntityNotificationHub<Address>>("ws/addresses");
        application.MapHub<EntityNotificationHub<Chief>>("ws/chiefs");
        application.MapHub<EntityNotificationHub<Contract>>("ws/contracts");
        application.MapHub<EntityNotificationHub<Customer>>("ws/customers");
        application.MapHub<EntityNotificationHub<Order>>("ws/orders");
        application.MapHub<EntityNotificationHub<Product>>("ws/products");
        application.MapHub<EntityNotificationHub<Workshop>>("ws/workshops");
    }
}