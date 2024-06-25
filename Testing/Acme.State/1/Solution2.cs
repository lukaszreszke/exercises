using Microsoft.EntityFrameworkCore;

namespace Acme.State;

public abstract class Humidifier2
{
    public HumidifierState State { get; set; }
    public int Id { get; set; }
}


public class HumidifierOn : Humidifier2
{
    public HumidifierOn()
    {
        State = HumidifierState.On;
    }

    public HumidifierOff TurnOff()
    {
        return new HumidifierOff();
    }

    public HumidifierNeedsCleaning NeedsCleaning()
    {
        return new HumidifierNeedsCleaning();
    }

    public HumidifierWaterShortage WaterShortage()
    {
        return new HumidifierWaterShortage();
    }

    public HumidifierOn FillWater()
    {
        return new HumidifierOn();
    }
}

public class HumidifierOff : Humidifier2
{
    public HumidifierState State { get; private set; }

    public HumidifierOff()
    {
        State = HumidifierState.Off;
    }
    
    public void TurnOn()
    {
        State = HumidifierState.On;
    }
}
 
public class HumidifierNeedsCleaning : Humidifier2
{
    public HumidifierState State { get; private set; }

    public HumidifierNeedsCleaning()
    {
        State = HumidifierState.NeedsCleaning;
    }

    public void Clean()
    {
        State = HumidifierState.On;
    }
}

public class HumidifierWaterShortage : Humidifier2
{
    public HumidifierState State { get; private set; }

    public HumidifierWaterShortage()
    {
        State = HumidifierState.WaterShortage;
    }

    public void FillWater()
    {
        State = HumidifierState.On;
    }
}


public class HumidifierContext : DbContext
{
    public DbSet<Humidifier2> Humidifiers { get; set; }

    public HumidifierContext(DbContextOptions<HumidifierContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Humidifier2>()
            .HasDiscriminator(x => x.State)
            .HasValue<HumidifierOn>(HumidifierState.On)
            .HasValue<HumidifierOff>(HumidifierState.Off)
            .HasValue<HumidifierNeedsCleaning>(HumidifierState.NeedsCleaning)
            .HasValue<HumidifierWaterShortage>(HumidifierState.WaterShortage);
    }
}