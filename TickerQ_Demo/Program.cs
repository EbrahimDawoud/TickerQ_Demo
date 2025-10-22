using Application.Interfaces;
using Application.Jobs;
using Application.Repositories;
using Application.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DependencyInjection;
using TickerQ_Demo.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TickerQ Demo API", Version = "v1" });
});


builder.Services.AddTickerQ(options=>
{
    options.SetMaxConcurrency(4);
    options.AddOperationalStore<ApplicationDbContext>(efOpt =>
    {
        efOpt.UseModelCustomizerForMigrations();
        //efOpt.CancelMissedTickersOnAppStart();
    });
    options.AddDashboard(options =>
    {
        options.BasePath = "/dashboard";
        options.EnableBasicAuth = true;
    });
    options.SetExceptionHandler<TickerExceptionHandler>();
});

// Repository DI
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Services DI
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<BackgroundJobs>();
builder.Services.AddScoped<JobWithData>();
builder.Services.AddScoped<ExceptionJob>();
builder.Services.AddScoped<CronJob>();



var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Enable TickerQ with Dashboard
app.UseTickerQ(qStartMode: TickerQ.Utilities.Enums.TickerQStartMode.Immediate);

app.MapControllers();
app.Run();
