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
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<LoginCommandHandler>();
    cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetUsersQuery>();
    //cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddDbContextInfraestructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtService, JwtService>();



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
