using WebApi.Context;
using WebApi.Contracts;
using WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your database context and repository for FootballTeam and Player.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IFootballTeamRepository, FootballTeamRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

// Configure Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map your controllers, which should now be in FootballTeamsController.
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    // ????????? middleware ??? ??????????????? ?? Swagger UI
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
        }
        else
        {
            await next();
        }
    });
}

app.Run();
