using System;
using System.Net.Http;
using System.Reflection;
using Beezy.BackendTest.Api.Behaviours;
using Beezy.BackendTest.Api.Services;
using Beezy.BackendTest.Domain;
using Beezy.BackendTest.Domain.Proxies;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using Beezy.BackendTest.Domain.Repositories;
using Beezy.BackendTest.Infrastructure.CrossCutting.HealthCheck;
using Beezy.BackendTest.Infrastructure.CrossCutting.Logging;
using Beezy.BackendTest.Infrastructure.CrossCutting.Swagger;
using Beezy.BackendTest.Infrastructure.CrossCutting.Settings;
using Beezy.BackendTest.Infrastructure.Data.DbContext;
using Beezy.BackendTest.Infrastructure.Data.Proxies;
using Beezy.BackendTest.Infrastructure.Data.Repositories;
using FluentValidation;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
#pragma warning disable 1591

namespace Beezy.BackendTest.Api.Configurations
{
    public static class ApiConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            services.AddMediatR(typeof(GetIntelligentBillboardHandler).GetTypeInfo().Assembly);
            services.AddSingleton(serviceProvider => GetTheMovieDbApiSettings(configuration));
            services.AddApiHealthChecks();
            services.AddSingleton<IDateService, DateService>();
            services.AddSingleton<ITheMovieDbProxy, TheMovieDbProxy>();
            services.AddScoped<IBeezyCinemaRepository, BeezyCinemaRepository>();
            services.AddPipelineBehaviors();
            services.AddProblemDetails(pdo => ConfigureProblemDetails(pdo, environment));
            services.AddControllers();
            services.AddVersioning();
            services.AddSwagger();
            services.ConfigureLogger(configuration);
            services.AddDbContext<BeezyCinemaContext>(options =>
                options.UseSqlServer(GetDatabaseSettings(configuration).ConnectionString));
        }

        public static void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseStaticFiles();

            app.UseMiddleware<LogContextMiddleware>(); // This should go in this order

            app.UseRouting();
            app.UseProblemDetails();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }

        private static TheMovieDbApiSettings GetTheMovieDbApiSettings(IConfiguration configuration) =>
            configuration.GetSection("TheMovieDbApiSettings").Get<TheMovieDbApiSettings>();

        private static DatabaseSettings GetDatabaseSettings(IConfiguration configuration) =>
            configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();

        public static void AddApiHealthChecks(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck<WhatchdogFileHealthCheck>("Watchdog File Check", HealthStatus.Unhealthy,
                    new[] { "watchdog", "file" });
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    //integrate xml comments
                    options.IncludeXmlComments(
                        $@"{AppDomain.CurrentDomain.BaseDirectory}\Beezy.BackendTest.Api.xml");
                });
        }

        private static void ConfigureProblemDetails(ProblemDetailsOptions options, IWebHostEnvironment environment)
        {
            // This is the default behavior; only include exception details in a development environment.
            options.IncludeExceptionDetails = (ctx, ex) => environment.IsDevelopment();

            // This will map NotImplementedException to the 501 Not Implemented status code.
            options.Map<NotImplementedException>(ex => new StatusCodeProblemDetails(StatusCodes.Status501NotImplemented));

            // This will map HttpRequestException to the 503 Service Unavailable status code.
            options.Map<HttpRequestException>(ex => new StatusCodeProblemDetails(StatusCodes.Status503ServiceUnavailable));

            // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
            // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
            options.Map<Exception>(ex => new StatusCodeProblemDetails(StatusCodes.Status500InternalServerError));
        }

        public static void AddPipelineBehaviors(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineBehavior<,>));

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(GetIntelligentBillboardHandler))
                .AddClasses(@class => @class.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces());
        }

        public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Information()
                .WriteTo.Console(
                    LogEventLevel.Information,
                    "{Timestamp:HH:mm:ss(zzz)} [{Level:u3}]: {Message} {Exception}"
                )
                .WriteTo.File(
                    outputTemplate: "{NewLine}{Timestamp:HH:mm:ss(zzz)} [{Level:u3}] ({SourceContext}): {Message} {Exception}",
                    path: configuration.GetSection("Logging:Path").Value,
                    rollOnFileSizeLimit: true,
                    rollingInterval: RollingInterval.Day,
                    shared: true,
                    restrictedToMinimumLevel: LogEventLevel.Debug)
                .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger: logger, dispose: true));
        }
    }
}
