using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MinimalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/usuario", async (MinimalContext context) =>
{
    try
    {
        var usuarios = await context.Usuarios.ToListAsync();
        return Results.Ok(usuarios);
    }
    catch (Exception)
    {
        throw new Exception("Erro ao recuperar lista de Usuários");
    }
}).Produces<Usuario>(StatusCodes.Status200OK)
  .Produces<Usuario>(StatusCodes.Status404NotFound);

app.MapGet("/usuario/{id}", async (int id, MinimalContext context) =>
    await context.Usuarios.FindAsync(id)
        is Usuario usuario
        ? Results.Ok(usuario)
        : Results.NotFound())
    .Produces<Usuario>(StatusCodes.Status200OK)
    .Produces<Usuario>(StatusCodes.Status404NotFound);

app.Run();