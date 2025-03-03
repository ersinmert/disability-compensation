using DisabilityCompensation.Persistence;
using DisabilityCompensation.Application;
using DisabilityCompensation.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInjectionApplication(builder.Configuration);
builder.Services.AddInjectionPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.AddMiddleware();
app.MapControllers();

app.Run();
