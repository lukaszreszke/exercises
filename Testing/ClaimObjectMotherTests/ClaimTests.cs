using System;
using ClaimObjectMother;
using Xunit;

public class ClaimTests
{
    [Fact]
    public void ApproveClaim()
    {
        var claim = new Claim(1, "ABC123", 1000.00m, DateTime.Now.AddDays(-15));
        claim.ApproveClaim();
        Assert.True(claim.IsApproved);
    }

    [Fact]
    public void CannotApproveClaimExceedingAmountLimit()
    {
        var claim = new Claim(2, "XYZ789", 6000.00m, DateTime.Now.AddDays(-15));
        Assert.Throws<InvalidOperationException>(() => claim.ApproveClaim());
    }

    [Fact]
    public void CannotApproveClaimExceedingDaysLimit()
    {
        var claim = new Claim(3, "DEF456", 3000.00m, DateTime.Now.AddDays(-40));
        Assert.Throws<InvalidOperationException>(() => claim.ApproveClaim());
    }
}