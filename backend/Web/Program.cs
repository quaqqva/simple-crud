using Backend.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(Directory.GetParent(Directory.GetCurrentDirectory()) + "/appsettings.json");
builder.Services.AddConfiguration(builder.Configuration);
var app = builder.Build();
app.AddConfiguration();
app.Run();
