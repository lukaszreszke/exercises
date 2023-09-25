namespace Insurances.Models;

public class Insurance
{
    public int PolicyNumber { get; set; }
    public string PolicyHolder { get; set; }
    public double PremiumAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public InsuranceType PolicyType { get; set; }
    public InsuranceRegion Region { get; set; }
    public bool HasPreviousClaims { get; set; }
    
    public Insurance(int policyNumber, string policyHolder, double premiumAmount, DateTime startDate, DateTime endDate)
    {
        PolicyNumber = policyNumber;
        PolicyHolder = policyHolder;
        PremiumAmount = premiumAmount;
        StartDate = startDate;
        EndDate = endDate;
    }
}

public enum InsuranceType
{
    Auto,
    Health,
    Home,
    Life
}

public enum InsuranceRegion
{
    LowRisk,
    MediumRisk,
    HighRisk
}
