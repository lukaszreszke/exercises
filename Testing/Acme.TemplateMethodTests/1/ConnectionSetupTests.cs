using Acme.TemplateMethod;

namespace Acme.TemplateMethodTests; 

public class ConnectionSetupTests
{
    [Fact]
    public void EstablishConnection_TCPConnection_PerformsHandshake()
    {
        var tcpConnection = new TCPConnectionSetup();
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        tcpConnection.EstablishConnection();

        string output = consoleOutput.ToString();
        Assert.Contains("Performing TCP handshake", output);
    }

    [Fact]
    public void EstablishConnection_UDPConnection_DoesNotPerformHandshake()
    {
        var udpConnection = new UDPConnectionSetup();
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        udpConnection.EstablishConnection();

        string output = consoleOutput.ToString();
        Assert.DoesNotContain("Performing", output);
    }

    [Fact]
    public void EstablishConnection_WebSocketConnection_PerformsHandshake()
    {
        var webSocketConnection = new WebSocketConnectionSetup();
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        webSocketConnection.EstablishConnection();

        string output = consoleOutput.ToString();
        Assert.Contains("Performing WebSocket handshake", output);
    }
}