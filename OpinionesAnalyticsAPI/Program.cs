using Microsoft.EntityFrameworkCore;
using OpinionesAnalyticsAPI.DATA.Context;
using OpinionesAnalyticsAPI.DATA.Interface;
using OpinionesAnalyticsAPI.DATA.Logging;
using OpinionesAnalyticsAPI.DATA.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<Social_CommentsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Social_CommentsConnection"))
);

builder.Services.AddSingleton(typeof(ILoggerBase<>), typeof(LoggerBase<>));

builder.Services.AddScoped<ISocial_CommentsRepository, Social_CommentsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
