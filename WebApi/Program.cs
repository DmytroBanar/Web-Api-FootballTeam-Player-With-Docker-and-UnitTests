using WebApi.Context;
using WebApi.Contracts;
using WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your database context and repository for FootballTeam and Player.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IFootballTeamRepository, FootballTeamRepository>();

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

app.Run();
