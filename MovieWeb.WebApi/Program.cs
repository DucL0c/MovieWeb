using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Data;
using MovieWeb.Data.Repository;
using MovieWeb.Service;
using MovieWeb.WebApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// autofac
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ContainerModule());
    builder.RegisterInstance(AutoMapperConfig.Initialize()).As<IMapper>().SingleInstance();
    builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
    builder.RegisterAssemblyTypes(typeof(SystemGroupRepository).Assembly)
          .Where(t => t.Name.EndsWith("Repository"))
           .As(serviceMapping: x => x.GetInterfaces().FirstOrDefault(t => t.Name.EndsWith("Repository")));
    builder.RegisterAssemblyTypes(typeof(SystemGroupService).Assembly)
       .Where(t => t.Name.EndsWith("Service"))
       .As(x => x.GetInterfaces().FirstOrDefault(t => t.Name.EndsWith("Service")));
}).ConfigureServices(services =>
{
    services.AddAutofac();
});
builder.Services.AddControllersWithViews();

//JWT
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();

app.Run();