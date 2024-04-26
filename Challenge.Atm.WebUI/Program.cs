using Challenge.Atm.Application.Handlers;
using Challenge.Atm.Application.Services;
using Challenge.Atm.Application.Validations;
using Challenge.Atm.Domain.EF.DBContexts;
using Challenge.Atm.Domain.EF.Repositories;
using Challenge.Atm.Domain.Interfaces;
using Challenge.Atm.Infrastructure;
using Challenge.Atm.WebUI.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<LoginCommandHandler>();
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtService, JwtService>();


builder.Services.AddDbContextInfraestructure(builder.Configuration);
builder.Services.AddSharedtInfraestructure(builder.Configuration);
builder.Services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsyc<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();
