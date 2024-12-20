﻿using BreweryTrackingSystem.models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add cours policy - in a production app lock this down
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
    builder =>
    {
        builder.AllowAnyOrigin()
        .WithMethods("POST", "PUT", "DELETE", "GET", "OPTIONS")
        .AllowAnyHeader();
    });
});

//Adding the dbcontext to the service
builder.Services.AddDbContext<BitsContext>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

//Use in production version
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



