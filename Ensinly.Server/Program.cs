using Ensinly.Server.DAO;
using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Facade;
using Ensinly.Server.Facade.Inteface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IDAODatabase, DAOSQLServer>();
builder.Services.AddSingleton<IDAOUsuario, DAOUsuario>();
builder.Services.AddSingleton<IDAOAluno, DAOAluno>();

builder.Services.AddSingleton<IFacadeUsuario, FacadeUsuario>();
builder.Services.AddSingleton<IFacadeAluno, FacadeAluno>();


var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
