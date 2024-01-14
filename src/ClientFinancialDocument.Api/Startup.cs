using ClientFinancialDocument.Application;
using ClientFinancialDocument.Infrastructure;
using ClientFinancialDocument.Api.Middleware;

namespace ClientFinancialDocument.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var compName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettings.{compName}.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.tap.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddLogging(logging =>
            {
                logging.AddConfiguration(Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            });

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAuthorization();

            services.AddApplication();
            services.AddInfrastructure();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
            {
                app.UseCors(builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                var corsOrigin = Configuration["AppSettings:AllowedOrigins"] ?? "*";
                app.UseCors(builder =>
                   builder
                       .WithOrigins(corsOrigin.Split(',').Select(o => o.Trim()).ToArray())
                       .AllowAnyMethod()
                       .AllowAnyHeader());
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
