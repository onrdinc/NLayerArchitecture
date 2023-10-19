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
using Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


ConfigurationManager configuration = builder.Configuration;
string jwtKey = configuration.GetSection("Jwt:Key").Value;
string jwtIssuer = configuration.GetSection("Jwt:Issuer").Value;
string jwtAudience = configuration.GetSection("Jwt:Audience").Value;


var connectionString = builder.Configuration.GetConnectionString("myDb");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));


builder.Services.Configure<AppSettings>(o =>
{

    o.Jwt.Key = jwtKey;
    o.Jwt.Issuer = jwtIssuer;
    o.Jwt.Audience = jwtAudience;




});

builder.Services.AddAutoMapper(typeof(BankProfile));

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<DataContext>()));
builder.Services.AddScoped<IBankBs, BankBs>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<IUserBs, UserBs>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
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




builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = ctx =>
            {
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddControllers().
                AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.AddDependencyResolvers(new ICoreModule[]
{
                new CoreModule(),
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Purchasing.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7173")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "purchasing.API v1"));
}


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
