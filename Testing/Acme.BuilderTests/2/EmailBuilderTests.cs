using Xunit;
using System.Collections.Generic;
using Acme.Builder._2;

namespace Acme.BuilderTests._2;

public class EmailBuilderTests
{
    [Fact]
    public void ShouldBuildBasicEmail()
    {
        // Arrange & Act
        EmailSol email = new EmailBuilder()
            .WithSubject("Greetings")
            .WithBody("Hello, welcome to our platform.")
            .WithRecipient("user@example.com")
            .WithSender("info@example.com")
            .Build();

        // Assert
        Assert.Equal("Greetings", email.Subject);
        Assert.Equal("Hello, welcome to our platform.", email.Body);
        Assert.Equal("user@example.com", email.Recipient);
        Assert.Equal("info@example.com", email.Sender);
        Assert.Empty(email.Attachments);
    }

    [Fact]
    public void ShouldBuildEmailWithAttachments()
    {
        // Arrange & Act
        EmailSol email = new EmailBuilder()
            .WithSubject("Monthly Newsletter")
            .WithBody("Check out our latest newsletter.")
            .WithRecipient("subscriber@example.com")
            .WithSender("newsletter@example.com")
            .AddAttachment("newsletter.docx")
            .AddAttachment("image.jpg")
            .Build();

        // Assert
        Assert.Equal("Monthly Newsletter", email.Subject);
        Assert.Equal("Check out our latest newsletter.", email.Body);
    }
}