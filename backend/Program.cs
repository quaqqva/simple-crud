using backend.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string password = File.ReadAllText("/run/secrets/db-password");
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!.Replace("{password}", password);
builder.Services.AddDbContext<TypographyContext>(options => options.UseMySql(connection, ServerVersion.Parse("8.0.34-mysql")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
