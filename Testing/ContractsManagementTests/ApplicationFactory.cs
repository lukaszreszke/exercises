using ContractsManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContractsManagementTests;

public class ApplicationFactory : WebApplicationFactory<Program>
{
   protected override void ConfigureWebHost(IWebHostBuilder builder)
   {
      builder.ConfigureServices(services =>
      {
         var options = new DbContextOptionsBuilder<ContractsContext>()
            .UseInMemoryDatabase(databaseName: "contracts")
            .Options;
         services.AddSingleton(options);
         services.AddScoped<ContractsContext>();
      });
      base.ConfigureWebHost(builder);
   }
}