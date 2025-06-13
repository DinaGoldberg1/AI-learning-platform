using AutoMapper;
using BLL;
using BLL.API;
using BLL.Services;
using DAL.API;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

// הוספת מדיניות CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddScoped<ISubCategoryServiceBLL, SubCategoryServiceBLL>();
builder.Services.AddScoped<ISubCategoryServiceDAL, SubCategoryServiceDAL>();
builder.Services.AddScoped<IUserServiceBLL, UserServiceBLL>();
builder.Services.AddScoped<IUserServiceDAL, UserServiceDAL>();
builder.Services.AddScoped<ICategoryServiceBLL, CategoryServiceBLL>();
builder.Services.AddScoped<ICategoryServiceDAL, CategoryServiceDAL>();
builder.Services.AddScoped<IPromptServiceBLL, PromptServiceBLL>();
builder.Services.AddScoped<IPromptServiceDAL, PromptServiceDAL>();

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

// הוספת CORS לפני Authorization
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
