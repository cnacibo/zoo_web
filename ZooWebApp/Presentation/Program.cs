using Microsoft.AspNetCore.Mvc;
using ZooWebApp.Application.Interfaces;
using ZooWebApp.Application.Services;
using ZooWebApp.Infrastructure.Repositories;
using ZooWebApp.Background.FeedingTimeMonitor;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register repositories
builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();

// Register services
builder.Services.AddScoped<IAnimalTransferService, AnimalTransferService>();
builder.Services.AddScoped<IFeedingOrganizationService, FeedingOrganizationService>();
builder.Services.AddScoped<IZooStatisticsService, ZooStatisticsService>();
builder.Services.AddScoped<IAnimalFactory, AnimalFactory>();
builder.Services.AddScoped<IEnclosureFactory, EnclosureFactory>();
builder.Services.AddScoped<IFeedingScheduleFactory, FeedingScheduleFactory>();
builder.Services.AddHostedService<FeedingTimeMonitorService>();


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