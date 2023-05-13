using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers;
using Testcontainers.PostgreSql;
using Xunit;
using DbTemplate;
using Microsoft.EntityFrameworkCore;

namespace TestContainersExample
{
    public class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {

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
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}