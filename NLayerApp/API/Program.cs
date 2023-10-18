using Business.Implementations;
using Business.Interfaces;
using Business.Profiles;
using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Infrastructure.CrossCuttingConcerns.Caching;
using Infrastructure.UnitOfWorks.Implementations;
using Infrastructure.UnitOfWorks.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("myDb");
builder.Services.AddDbContext<DataContext>(options=>options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(BankProfile));

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<DataContext>()));
builder.Services.AddScoped<IBankBs, BankBs>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<ICache, MemoryCacheManager>();

//builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer("server=.\\SQLEXPRESS;database=Test;trusted_connection=true; trustServerCertificate=true;"));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Error()
                .CreateLogger();

//.MinimumLevel.Error()
builder.Host.UseSerilog();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
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
