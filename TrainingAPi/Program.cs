using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TrainingAPi.Extesnions2;
using TrainingAPi.Shared;
using TrainingAPi.Validators;
using TrainingApiDAL.Models;
using TrainingApiDAL.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TrainingConnectionString");
//var connec = builder.Configuration["ConnectionStrings:TrainingConnectionString"];

builder.Services.AddDbContext<TrainingTestDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IAppUserRepositry, AppUserRepository>();
builder.Services.AddScoped<IPostsRepository, PostsRepository>();
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddMapster();
// Add services to the container.

builder.Services.AddControllers();
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});

builder.Services.AddValidatorsFromAssemblyContaining<AppUserValidtor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExt();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
