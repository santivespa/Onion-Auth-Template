using Application;
using Shared;
using Persistence;
using WebAPI.Extensions;

var customSpecificOrigin = "customSpecificOrigin";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Custom Extensions
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddSharedInfra(configuration);
builder.Services.AddApiVersioningExtension();

//Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: customSpecificOrigin, builder =>
    {
        builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});



var app = builder.Build();

app.UseCors(customSpecificOrigin);

app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
