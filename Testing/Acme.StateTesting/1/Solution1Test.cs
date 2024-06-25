using Acme.State.Solution1;
using Microsoft.EntityFrameworkCore;

namespace Acme.StateTesting.Solution1;

public class UnitTest1
{
    [Fact]
    public void state_changes_with_db_context()
    {
        var options = new DbContextOptionsBuilder<HumidifierContext>()
            .UseInMemoryDatabase(databaseName: "HumidityDatabase")
            .Options;
        var context = new HumidifierContext(options);
        var humidifier = new Humidifier();

        humidifier.TurnOn();

        humidifier.TurnOff();

        context.Humidifiers.Add(humidifier);
        context.SaveChanges();

        Assert.Equal(1, humidifier.Id);
        Assert.Equal(typeof(OffState), humidifier.State.GetType());

        var humidifier2 = context.Humidifiers.Find(1);
        
        humidifier2.TurnOn();
        Assert.Equal(typeof(OnState), humidifier2.State.GetType());
        
        humidifier2.NeedsCleaning();
        Assert.Equal(typeof(NeedsCleaningState), humidifier2.State.GetType());

        Assert.Throws<InvalidStateChange>(() => humidifier.TurnOn());
    }
    
        [Fact]
        public void can_be_switched_on_when_off()
        {
            var humidifier = new Humidifier();
    
            humidifier.TurnOn();
    
            Assert.IsType<OnState>(humidifier.State);
        }
    
        [Fact]
        public void can_be_switched_off_when_on()
        {
            var humidifier = new Humidifier();
            humidifier.TurnOn();
    
            humidifier.TurnOff();
    
            Assert.IsType<OffState>(humidifier.State);
        }
    
        [Fact]
        public void can_be_switched_to_needs_cleaning_when_on()
        {
            var humidifier = new Humidifier();
            humidifier.TurnOn();
    
            humidifier.NeedsCleaning();
    
            Assert.IsType<NeedsCleaningState>(humidifier.State);
        }
    
        [Fact]
        public void gets_back_to_on_state_when_cleaned()
        {
            var humidifier = new Humidifier();
            humidifier.TurnOn();
            humidifier.NeedsCleaning();
    
            humidifier.Clean();
    
            Assert.IsType<OnState>(humidifier.State);
        }
    
        [Fact]
        public void can_have_water_shortage_when_on()
        {
            var humidifier = new Humidifier();
            humidifier.TurnOn();
    
            humidifier.WaterShortage();
    
            Assert.IsType<WaterShortageState>(humidifier.State);
        }
    
        [Fact]
        public void changes_to_on_when_water_filled()
        {
            var humidifier = new Humidifier();
            humidifier.TurnOn();
            humidifier.WaterShortage();
    
            humidifier.FillWater();
    
            Assert.IsType<OnState>(humidifier.State);
        }
    
        [Fact]
        public void cannot_transfer_from_on_to_cleaning()
        {
            var humidifier = new Humidifier();
    
            Assert.Throws<InvalidStateChange>(() => humidifier.Clean());
        }
        
        [Fact]
        public void cannot_transfer_from_off_to_fill_water()
        {
            var humidifier = new Humidifier();
    
            Assert.Throws<InvalidStateChange>(() => humidifier.FillWater());
        }
}