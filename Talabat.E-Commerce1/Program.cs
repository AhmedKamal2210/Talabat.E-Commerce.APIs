using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.E_Commerce1.Extensions;
using Talabat.E_Commerce1.MiddleWares;
using Talabat.Repository;

namespace Talabat.E_Commerce1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            #region Configure Services            

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });

            // Identity Connection String
            builder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            // Local Host for Caching Service.
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("RedisConnection"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDecumentation();
            //builder.Services.AddSwaggerGen();

            #endregion

            var app = builder.Build();

            #region Update Migration To Database When Running The Application
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreDbContext>(); // asking the CLR for creating object from DbContext Explicity.

            var _identityContext = services.GetRequiredService<IdentityDbContext>();
            var userManger = services.GetRequiredService<UserManager<AppUser>>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync(); // Update-Database
                await StoreContextSeed.SeedAsync(_dbcontext); // Data Seeding
                await IdentityContextSeed.SeedUserAsync(userManger);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration !");
            }
            #endregion

            // Configure the HTTP request pipeline.
            #region Configure Kestrel MiddleWares
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseMiddleware<ExceptionMiddleWare>();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}