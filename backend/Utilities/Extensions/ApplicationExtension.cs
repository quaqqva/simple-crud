using System.Net;
using backend.Database.Exceptions;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Diagnostics;

namespace backend.Utilities.Extensions
{
    public static class ApplicationExtension
    {
        public static void MabWebsocketHubs(this WebApplication application)
        {
            application.MapHub<EntityNotificationHub<Address>>("ws/addresses");
            application.MapHub<EntityNotificationHub<Chief>>("ws/chiefs");
            application.MapHub<EntityNotificationHub<Contract>>("ws/contracts");
            application.MapHub<EntityNotificationHub<Customer>>("ws/customers");
            application.MapHub<EntityNotificationHub<Order>>("ws/orders");
            application.MapHub<EntityNotificationHub<Product>>("ws/products");
            application.MapHub<EntityNotificationHub<Workshop>>("ws/workshops");
        }

        public static void SetupExceptionHandling(this WebApplication application)
        {
            _ = application.UseExceptionHandler(
                (config) =>
                {
                    config.Run(async context =>
                    {
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            var exception = error.Error;
                            HttpStatusCode status;
                            string errorMesage = exception.Message;

                            switch (exception)
                            {
                                case DbIntegrityException:
                                    status = HttpStatusCode.UnprocessableEntity;
                                    break;
                                case DbNotFoundException:
                                    status = HttpStatusCode.NotFound;
                                    break;
                                case NullReferenceException:
                                    status = HttpStatusCode.BadRequest;
                                    errorMesage = "Query parameters can't be empty";
                                    break;
                                case ArgumentException:
                                    status = HttpStatusCode.BadRequest;
                                    break;
                                default:
                                    status = HttpStatusCode.InternalServerError;
                                    break;
                            }

                            context.Response.StatusCode = (int)status;
                            await context.Response.WriteAsJsonAsync(
                                new ErrorResult()
                                {
                                    Status = status,
                                    Errors = new() { ["*"] = errorMesage }
                                }
                            );
                        }
                    });
                }
            );
        }
    }
}
