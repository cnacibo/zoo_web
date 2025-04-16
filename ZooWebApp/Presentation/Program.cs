using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;
using ZooWebApp.Application.Services;
using ZooWebApp.Infrastructure.Repositories;
using ZooWebApp.Domain.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register repositories
builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

// Register services
builder.Services.AddScoped<AnimalTransferService>();
builder.Services.AddScoped<FeedingOrganizationService>();
builder.Services.AddScoped<ZooStatisticsService>();

// Add Swagger
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
app.MapControllers();

app.Run();