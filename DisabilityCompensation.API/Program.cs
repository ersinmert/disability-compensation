using DisabilityCompensation.Persistence;
using DisabilityCompensation.Application;
using DisabilityCompensation.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInjectionPersistence(builder.Configuration);
builder.Services.AddInjectionApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.AddMiddleware();
app.MapControllers();

app.Run();
