using System.Text.Json.Serialization;
using backend.Database;
using backend.Entities;
using backend.WebSocket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
            builder.WithOrigins("http://localhost", "http://frontend");
        }
    );
});

builder
    .Services.AddControllers()
    .AddJsonOptions(
        (options) =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingDefault;
        }
    )
    .ConfigureApiBehaviorOptions(
        (options) =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var modelState = actionContext.ModelState;
                if (modelState.ContainsKey("$"))
                {
                    ModelStateEntry errorEntry;
                    modelState.TryGetValue("$", out errorEntry!);

                    string errorText = errorEntry.Errors.First().ErrorMessage;
                    string[] missingProperties = errorText.Split(':')[1].Split(',');
                    foreach (string property in missingProperties)
                    {
                        string formattedProperty = property.Trim();
                        modelState.AddModelError(formattedProperty, "Property is missing");
                    }
                    modelState.Remove("$");
                }
                if (modelState.ContainsKey("dto"))
                    modelState.Remove("dto");
                var result = new ValidationProblemDetails(modelState)
                {
                    Type = "https://courselibrary.com/modelvalidationproblem",
                    Title = "One or more model validation errors occurred.",
                };
                result.Extensions.Add("traceId", actionContext.HttpContext.TraceIdentifier);
                return new BadRequestObjectResult(result);
            };
        }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(typeof(EntityNotificationHubOperator<Address>));
builder.Services.AddSingleton(typeof(EntityNotificationHubOperator<Chief>));
builder.Services.AddSingleton(typeof(EntityNotificationHubOperator<Contract>));
builder.Services.AddSingleton(typeof(EntityNotificationHubOperator<Customer>));
builder.Services.AddSingleton(typeof(EntityNotificationHubOperator<Order>));
builder.Services.AddSingleton(typeof(EntityNotificationHubOperator<Product>));
builder.Services.AddSingleton(typeof(EntityNotificationHubOperator<Workshop>));

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.HandshakeTimeout = TimeSpan.FromMinutes(30);
});

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
string password = File.ReadAllText("/run/secrets/db-password");
connection = connection.Replace("{password}", password);
builder.Services.AddDbContext<TypographyContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version(8, 2, 0)))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else { }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapHub<EntityNotificationHub<Address>>("ws/addresses");
app.MapHub<EntityNotificationHub<Chief>>("ws/chiefs");
app.MapHub<EntityNotificationHub<Contract>>("ws/contracts");
app.MapHub<EntityNotificationHub<Customer>>("ws/customers");
app.MapHub<EntityNotificationHub<Order>>("ws/orders");
app.MapHub<EntityNotificationHub<Product>>("ws/products");
app.MapHub<EntityNotificationHub<Workshop>>("ws/workshops");

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
