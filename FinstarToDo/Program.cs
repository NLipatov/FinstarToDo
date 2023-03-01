using FinstarToDo.DAL.Extensions;
using FinstarToDo.DB;
using FinstarToDo.Services.HashCalculating.Extensions;
using Microsoft.AspNetCore.HttpLogging;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<ToDoContext>();
    builder.Services.UseHashService();
    builder.Services.UseDAL();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddHttpLogging(options =>
    {
        options.LoggingFields = HttpLoggingFields.RequestHeaders |
                                HttpLoggingFields.RequestBody |
                                HttpLoggingFields.ResponseHeaders |
                                HttpLoggingFields.ResponseBody;
    });

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

    app.UseHttpLogging();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}