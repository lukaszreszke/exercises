
using System.Linq;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace RoomBookingApp.Api.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;


public class BookingWebApplicationFactory<TStartup>
    : IClassFixture<WebApplicationFactory<TStartup>> where TStartup: class
{
    public BookingWebApplicationFactory(WebApplicationFactory<TStartup> factory)
    {
        factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<RoomBookingAppDbContext>(options =>
                {
                    InMemoryDbContextOptionsExtensions.UseInMemoryDatabase(options, "in_memory_database_testing");
                });
            });
        }).CreateClient();
    }
}



public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup: class
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var contextOptionsBuilder = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
        contextOptionsBuilder.UseInMemoryDatabase("in_memory");
        
        builder
            .ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<RoomBookingAppDbContext>));

            services.Remove(descriptor);
            
            services.AddDbContext<RoomBookingAppDbContext>(options =>
            {
                InMemoryDbContextOptionsExtensions.UseInMemoryDatabase(options, "in_memory_database_testing");
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var appDbContext = scopedServices.GetRequiredService<RoomBookingAppDbContext>();
                appDbContext.Database.EnsureCreated();
            }
        });
    }
}