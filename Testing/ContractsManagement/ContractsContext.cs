using Microsoft.EntityFrameworkCore;

namespace ContractsManagement;

public class ContractsContext : DbContext
{
    public ContractsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Contract> Contracts { get; set;  }
}