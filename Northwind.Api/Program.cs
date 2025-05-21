using Northwind.Application.Abstractions.Contracts;
using Northwind.Application.Services;
using Northwind.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(o => o.AddPolicy("mvc", p =>
{
    p.WithOrigins("https://localhost:5002")
     .AllowAnyHeader()
     .AllowAnyMethod();
}));

builder.Services.AddControllers();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("mvc");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
