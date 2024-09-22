using Autofac;
using Autofac.Extensions.DependencyInjection;
using Micromarin.Application.DependencyInjection;
using Micromarin.Domain.DependencyInjection;
using Micromarin.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllers();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacApplicationModule());
    containerBuilder.RegisterModule(new AutofacSharedModule());
    containerBuilder.RegisterModule(new AutofacInfrastructureModule(builder.Configuration));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
