using Microsoft.EntityFrameworkCore;

namespace Acme.State;

public enum HumidifierState
{
    Off,
    On,
    WaterShortage,
    NeedsCleaning
}

public class Humidifier
{
    public HumidifierState State { get; private set; }

    public Humidifier()
    {
        State = HumidifierState.Off;
    }

    public void TurnOn()
    {
        if (State == HumidifierState.Off || State == HumidifierState.WaterShortage ||
            State == HumidifierState.NeedsCleaning)
        {
            State = HumidifierState.On;
        }
        else
        {
            throw new System.Exception("Humidifier is already on");
        }
    }

    public void TurnOff()
    {
        if (State == HumidifierState.On)
        {
            State = HumidifierState.Off;
        }
        else
        {
            throw new System.Exception("Humidifier is already off");
        }
    }

    public void NeedsCleaning()
    {
        if (State == HumidifierState.On)
        {
            State = HumidifierState.NeedsCleaning;
        }
        else
        {
            throw new System.Exception("Humidifier is not on");
        }
    }

    public void Clean()
    {
        if (State == HumidifierState.NeedsCleaning)
        {
            State = HumidifierState.On;
        }
        else
        {
            throw new System.Exception("Humidifier does not need cleaning");
        }
    }

    public void WaterShortage()
    {
        if (State == HumidifierState.On)
        {
            State = HumidifierState.WaterShortage;
        }
        else
        {
            throw new System.Exception("Humidifier is not on");
        }
    }

    public void FillWater()
    {
        if (State == HumidifierState.WaterShortage)
        {
            State = HumidifierState.On;
        }
        else
        {
            throw new System.Exception("Humidifier does not need water");
        }
    }
}