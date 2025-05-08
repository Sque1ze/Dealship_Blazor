using Dealship.Api.Data;
using Dealship.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDealship();

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string ClientOrigin = "client";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(ClientOrigin, p =>
        p.WithOrigins("https://localhost:7209")   // Blazor WASM
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(ClientOrigin);

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
