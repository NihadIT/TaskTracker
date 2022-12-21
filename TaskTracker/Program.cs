using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskTracker.Models;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

// Connection string (located in the json file)
string connection = configuration.GetConnectionString("DefaultConnection");

// Connection to the base
builder.Services.AddDbContext<TaskDbContext>(o =>
{
    o.UseSqlServer(connection);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Eliminates cycling
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    //Add Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


