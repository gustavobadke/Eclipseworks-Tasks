using Eclipseworks.Tasks.Api.Authentication;
using Eclipseworks.Tasks.Application.Security;
using Eclipseworks.Tasks.Infrastructure.Registers;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Eclipseworks.Tasks.Application.Mappers;
using System.Reflection;
using Eclipseworks.Tasks.Application.Configuration;
using Eclipseworks.Tasks.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(SimpleAuthenticationOptions.DefaultScheme)
    .AddScheme<SimpleAuthenticationOptions, SimpleAuthenticationHandler>(
        SimpleAuthenticationOptions.DefaultScheme, options => { });

builder.Services.Configure<AppOptions>(builder.Configuration.GetSection("AppSettings"));

var appOptions = new AppOptions();
builder.Configuration.GetSection("AppSettings").Bind(appOptions);

builder.Services.AddRepositories(appOptions);

builder.Services.AddMappings();

var appAssembly = Assembly.Load("Eclipseworks.Tasks.Application");
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(appAssembly));
builder.Services.AddFluentValidation(new[] { appAssembly });

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthentication, SimpleAuthentication>();

builder.Services.AddControllers();

builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();