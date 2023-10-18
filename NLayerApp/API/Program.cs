using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers;
using Business.Implementations;
using Business.Interfaces;
using Business.Profiles;
using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Infrastructure.DependecyResolvers;
using Infrastructure.UnitOfWorks.Implementations;
using Infrastructure.UnitOfWorks.Interface;
using Infrastructure.Utilites.IoC;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Infrastructure.Extensions;
var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("myDb");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));




builder.Services.AddAutoMapper(typeof(BankProfile));

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<DataContext>()));
builder.Services.AddScoped<IBankBs, BankBs>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
//builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();

//builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer("server=.\\SQLEXPRESS;database=Test;trusted_connection=true; trustServerCertificate=true;"));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Error()
                .CreateLogger();

//.MinimumLevel.Error()
builder.Host.UseSerilog();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>
    (containerBuilder => containerBuilder.RegisterModule(new AutofacBusinessModule()));



builder.Services.AddControllers();



builder.Services.AddDependencyResolvers(new ICoreModule[]
{
                new CoreModule(),
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
