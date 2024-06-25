using Microsoft.EntityFrameworkCore;

namespace Acme.State.Solution1;

public abstract class HumidifierState
{
    public virtual void TurnOn(Humidifier humidifier)
    {
        throw new InvalidStateChange();
    }

    public virtual void TurnOff(Humidifier humidifier)
    {
        throw new InvalidStateChange();
    }

    public virtual void NeedsCleaning(Humidifier humidifier)
    {
        throw new InvalidStateChange();
    }

    public virtual void Clean(Humidifier humidifier)
    {
        throw new InvalidStateChange();
    }

    public virtual void WaterShortage(Humidifier humidifier)
    {
        throw new InvalidStateChange();
    }

    public virtual void FillWater(Humidifier humidifier)
    {
        throw new InvalidStateChange();
    }
}

public class InvalidStateChange : Exception
{
}

public class OffState : HumidifierState
{
    public override void TurnOn(Humidifier humidifier)
    {
        humidifier.State = new OnState();
    }
}

public class OnState : HumidifierState
{
    public override void TurnOff(Humidifier humidifier)
    {
        humidifier.State = new OffState();
    }

    public override void NeedsCleaning(Humidifier humidifier)
    {
        humidifier.State = new NeedsCleaningState();
    }

    public override void WaterShortage(Humidifier humidifier)
    {
        humidifier.State = new WaterShortageState();
    }
}

public class NeedsCleaningState : HumidifierState
{
    public override void Clean(Humidifier humidifier)
    {
        humidifier.State = new OnState();
    }
}

public class WaterShortageState : HumidifierState
{
    public override void FillWater(Humidifier humidifier)
    {
        humidifier.State = new OnState();
    }
}

public class Humidifier
{
    public int Id { get; private set; }
    public HumidifierState State { get; internal set; }

    public Humidifier()
    {
        State = new OffState();
    }

    public void TurnOn()
    {
        State.TurnOn(this);
    }

    public void TurnOff()
    {
        State.TurnOff(this);
    }

    public void NeedsCleaning()
    {
        State.NeedsCleaning(this);
    }

    public void Clean()
    {
        State.Clean(this);
    }

    public void WaterShortage()
    {
        State.WaterShortage(this);
    }

    public void FillWater()
    {
        State.FillWater(this);
    }
}

public class HumidifierContext : DbContext
{
    public DbSet<Humidifier> Humidifiers { get; set; }

    public HumidifierContext(DbContextOptions<HumidifierContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Humidifier>().HasKey(x => x.Id);


        modelBuilder.Entity<Humidifier>()
            .Property(h => h.State)
            .HasConversion(h => h.GetType().Name, h => (HumidifierState)Activator.CreateInstance(Type.GetType(h)));
    }
}
