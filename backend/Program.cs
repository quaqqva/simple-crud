using backend.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers().ConfigureJsonSerializationOptions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureWebsocketServices();
builder.Services.ConfigureAuthenticationAndAuthorization(
    builder.Configuration["JwtSettings:Issuer"]!,
    builder.Configuration["JwtSettings:Audience"]!
);

string connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.SetupDatabaseConnection(connectionStringTemplate);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else { }

app.SetupExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MabWebsocketHubs();
app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
