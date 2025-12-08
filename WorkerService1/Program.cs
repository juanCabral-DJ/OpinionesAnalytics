using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Interfaces;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Application.Services;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Infrastructure.Logging;
using OpinionesAnalytics.Persistence.Dwh;
using OpinionesAnalytics.Persistence.Dwh.Context;
using OpinionesAnalytics.Persistence.Repositories.API;
using OpinionesAnalytics.Persistence.Repositories.Csv;
using OpinionesAnalytics.Persistence.Repositories.Db;
using OpinionesAnalytics.Persistence.Repositories.Db.Context;
using OpinionesAnalyticsAPI.DATA.Domain;
using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);


//Registro de dependencias   
builder.Services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));

var opinionesConn = builder.Configuration.GetConnectionString("OpinionesDB");
builder.Services.AddDbContext<ReviewsContext>(options =>
            options.UseSqlServer(opinionesConn));


var connectionString = builder.Configuration["DWHConnectionString:DWHDB"];
builder.Services.AddDbContextPool<DWHOpinionesContext>(options =>
options.UseSqlServer(connectionString)); 

builder.Services.AddScoped<IFileReaderRepository<surveys>, SurveysRepository>();
builder.Services.AddScoped<IFileReaderRepository<Productos>, ProductoRepository>();
builder.Services.AddScoped<IFileReaderRepository<Clientes>, ClienteRepository>(); 
builder.Services.AddScoped<IDwhRepository, DwhRepositories>(); 

//Registro de dependencias para el services
builder.Services.AddTransient<IOpinionesHandlerServices, OpinionHandlerServices>();

builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();
