namespace Acme.TemplateMethod;

using System;

public abstract class ConnectionSetup
{
    public void EstablishConnection()
    {
        MakeConnection();
        if (NeedsHandshake())
        {
            PerformHandshake();
        }
        ValidateConnection();
    }

    protected abstract void MakeConnection();
    protected abstract bool NeedsHandshake();
    protected abstract void PerformHandshake();
    protected abstract void ValidateConnection();
}

public class TCPConnectionSetup : ConnectionSetup
{
    protected override void MakeConnection()
    {
        Console.WriteLine("Establishing TCP connection");
    }

    protected override bool NeedsHandshake()
    {
        // TCP connection requires handshake
        return true;
    }

    protected override void PerformHandshake()
    {
        Console.WriteLine("Performing TCP handshake");
    }

    protected override void ValidateConnection()
    {
        Console.WriteLine("Validating TCP connection");
    }
}

public class UDPConnectionSetup : ConnectionSetup
{
    protected override void MakeConnection()
    {
        Console.WriteLine("Establishing UDP connection");
    }

    protected override bool NeedsHandshake()
    {
        // UDP connection does not require handshake
        return false;
    }

    protected override void PerformHandshake()
    {
        Console.WriteLine("No handshake needed for UDP");
    }

    protected override void ValidateConnection()
    {
        Console.WriteLine("Validating UDP connection");
    }
}

public class WebSocketConnectionSetup : ConnectionSetup
{
    protected override void MakeConnection()
    {
        Console.WriteLine("Establishing WebSocket connection");
    }

    protected override bool NeedsHandshake()
    {
        // WebSocket connection requires handshake
        return true;
    }

    protected override void PerformHandshake()
    {
        Console.WriteLine("Performing WebSocket handshake");
    }

    protected override void ValidateConnection()
    {
        Console.WriteLine("Validating WebSocket connection");
    }
}