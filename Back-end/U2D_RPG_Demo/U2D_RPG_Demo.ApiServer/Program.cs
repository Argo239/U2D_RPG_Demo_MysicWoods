using Microsoft.EntityFrameworkCore;
using U2D_RPG_Demo.ApiServer.Controllers;
using U2D_RPG_Demo.ApiServer.Interfaces;
using U2D_RPG_Demo.ApiServer.Models;
using U2D_RPG_Demo.ApiServer.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSwaggerGen();

// Register Services
builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddScoped<IPlayerAttributeRepository, PlayerAttributeRepository>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
