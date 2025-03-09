using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyNisienDrinkApi.Controllers; 
using MyNisienDrinkApi.Data;
using Microsoft.EntityFrameworkCore; 



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("DrinkOrderDb"));

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
