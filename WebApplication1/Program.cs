using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB
//var configdb = builder.Configuration.GetSection("ConnectionStrings=DockerDb").Value;
//var configdb = builder.Configuration.GetConnectionString("DockerDb");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(configdb));

var configdb = builder.Configuration.GetSection("ConnectionStrings:DockerDb").Value;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configdb));

// DI

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

app.MapGet("/Category", async (AppDbContext dbContext) =>
{
    return await dbContext.Categories.ToListAsync();
});



app.Run();
