using Ensinly.Server.DAO;
using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.Facade;
using Ensinly.Server.Facade.Inteface;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IDAODatabase, DAOSQLServer>();
builder.Services.AddSingleton<IDAOUsuario, DAOUsuario>();
builder.Services.AddSingleton<IDAOAluno, DAOAluno>();
builder.Services.AddSingleton<IDAOLogin, DAOLogin>();
builder.Services.AddSingleton<IDAOProfessor, DAOProfessor>();
builder.Services.AddSingleton<IDAONivelAprendizado, DAONivelAprendizado>();

builder.Services.AddSingleton<IFacadeUsuario, FacadeUsuario>();
builder.Services.AddSingleton<IFacadeAluno, FacadeAluno>();
builder.Services.AddSingleton<IFacadeLogin, FacadeLogin>();
builder.Services.AddSingleton<IFacadeProfessor, FacadeProfessor>();
builder.Services.AddSingleton<IFacadeNivelAprendizado, FacadeNivelAprendizado>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(op => {
    op.Cookie.Name = "session";
    op.Cookie.HttpOnly = true;
    op.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    op.Cookie.SameSite = SameSiteMode.Lax;
    op.ExpireTimeSpan = TimeSpan.FromHours(12);
    op.SlidingExpiration = true;

    op.Events.OnRedirectToLogin = ctx =>{
        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    op.Events.OnRedirectToAccessDenied = ctx =>{
        ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});


var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
