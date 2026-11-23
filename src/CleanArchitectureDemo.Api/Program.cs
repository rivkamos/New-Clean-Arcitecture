using CleanArchitectureDemo.Application;
using CleanArchitectureDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// רישום השכבות (Dependency Injection)
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
