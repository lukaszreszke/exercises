using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers;
using Testcontainers.PostgreSql;
using Xunit;
using DbTemplate;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Respawn;
using Npgsql;

namespace TestContainersExample
{
    public class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {

        public HttpClient HttpClient { get; private set; } = null!;

        private DbConnection _dbConnection = null!;
        private Respawner _respawner = null!;

        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithCleanUp(true)
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
                        {
                            var existingDescriptor = services.SingleOrDefault(
                                d => d.ServiceType == typeof(DbContextOptions<CustomerContext>));
                            if (existingDescriptor != null)
                            {
                                services.Remove(existingDescriptor);
                            }
                            var dbContextOptions = new DbContextOptionsBuilder<CustomerContext>()
                             .UseNpgsql(_dbContainer.GetConnectionString())
                             .Options;
                            services.AddSingleton(dbContextOptions);
                            services.AddScoped<CustomerContext>();
                        });
        }

        public CustomerContext GetCustomerContext()
        {
            return Services.CreateScope().ServiceProvider.GetRequiredService<CustomerContext>();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            using (var scope = Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<CustomerContext>();
                dbContext.Database.EnsureCreated();
            }
            HttpClient = CreateClient();
            _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
            await _dbConnection.OpenAsync();
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = new[] { "public" },
            });
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }
    }
}