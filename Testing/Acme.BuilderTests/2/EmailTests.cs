using Acme.Builder._2;

namespace Acme.BuilderTests._2;

using Xunit;
using System.Collections.Generic;

public class EmailTests
{
    [Fact]
    public void ShouldBuildBasicEmail()
    {
        // Arrange & Act
        Email email = new Email("Greetings");

        // Assert
        Assert.Equal("Greetings", email.Subject);
        Assert.Null(email.Body);
        Assert.Null(email.Recipient);
        Assert.Null(email.Sender);
        Assert.Null(email.Attachments);
    }

    [Fact]
    public void ShouldBuildEmailWithBody()
    {
        // Arrange & Act
        Email email = new Email("Greetings", "Hello, welcome to our platform.");

        // Assert
        Assert.Equal("Greetings", email.Subject);
        Assert.Equal("Hello, welcome to our platform.", email.Body);
        Assert.Null(email.Recipient);
        Assert.Null(email.Sender);
        Assert.Null(email.Attachments);
    }

    [Fact]
    public void ShouldBuildEmailWithRecipient()
    {
        // Arrange & Act
        Email email = new Email("Greetings", "Hello, welcome!", "user@example.com");

        // Assert
        Assert.Equal("Greetings", email.Subject);
        Assert.Equal("Hello, welcome!", email.Body);
        Assert.Equal("user@example.com", email.Recipient);
        Assert.Null(email.Sender);
        Assert.Null(email.Attachments);
    }

    [Fact]
    public void ShouldBuildEmailWithSenderAndAttachments()
    {
        // Arrange & Act
        List<string> attachments = new List<string> { "document.pdf", "image.jpg" };
        Email email = new Email("Greetings", "Hello, welcome!", "user@example.com", "info@example.com", attachments);

        // Assert
        Assert.Equal("Greetings", email.Subject);
        Assert.Equal("Hello, welcome!", email.Body);
        Assert.Equal("user@example.com", email.Recipient);
        Assert.Equal("info@example.com", email.Sender);
        Assert.Equal(attachments, email.Attachments);
    }
}
