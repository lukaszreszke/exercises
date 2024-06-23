using Acme.Builder;

namespace Acme.BuilderTests._1;

using Xunit;
using System.Collections.Generic;

public class ConfigurationBuilderTests
{
    [Fact]
    public void ShouldBuildBasicConfiguration()
    {
        // Arrange & Act
        Configuration configuration = new ConfigurationBuilder()
            .SetDatabaseConnection("sqlserver://localhost")
            .EnableLogging()
            .SetTimeout(30)
            .Build();

        // Assert
        Assert.Equal("sqlserver://localhost", configuration.DatabaseConnection);
        Assert.True(configuration.EnableLogging);
        Assert.Equal(30, configuration.Timeout);
        Assert.Empty(configuration.IncludedModules);
    }

    [Fact]
    public void ShouldBuildConfigurationWithModules()
    {
        // Arrange & Act
        Configuration configuration = new ConfigurationBuilder()
            .SetDatabaseConnection("mysql://127.0.0.1")
            .EnableLogging()
            .SetTimeout(60)
            .AddModule("Module A")
            .AddModule("Module B")
            .Build();

        // Assert
        Assert.Equal("mysql://127.0.0.1", configuration.DatabaseConnection);
        Assert.True(configuration.EnableLogging);
        Assert.Equal(60, configuration.Timeout);
        Assert.Equal(new List<string> { "Module A", "Module B" }, configuration.IncludedModules);
    }

    [Fact]
    public void ShouldBuildDefaultConfiguration()
    {
        // Arrange & Act
        Configuration configuration = new ConfigurationBuilder().Build();

        // Assert
        Assert.Null(configuration.DatabaseConnection);
        Assert.False(configuration.EnableLogging);
        Assert.Equal(0, configuration.Timeout);
        Assert.Empty(configuration.IncludedModules);
    }
}
