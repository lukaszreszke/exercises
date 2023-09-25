using Insurances.Models;

namespace Insurances.Services;

public class InsuranceService
{
    private readonly IInsuranceRepository _insuranceRepository;
    private readonly ILogger<InsuranceService> _logger;

    public InsuranceService(IInsuranceRepository insuranceRepository, ILogger<InsuranceService> logger)
    {
        _insuranceRepository = insuranceRepository;
        _logger = logger;
    }

    public void CalculateInsurance(int insuranceId, int policyHolderAge)
    {
        _logger.LogInformation("Starting");
        var insurance = _insuranceRepository.Find(insuranceId);
        _logger.LogInformation("Got obj!");
        double basePremium = insurance.PremiumAmount;

        if (policyHolderAge < 25)
        {
            _logger.LogInformation("Age under 25");
            basePremium *= 1.5;
        }
        else if (policyHolderAge > 60)
        {
            basePremium *= 1.2;
            _logger.LogInformation("Age above 60");
        }

        if (insurance.PolicyType == InsuranceType.Auto)
        {
            basePremium *= 1.3;
        }
        else if (insurance.PolicyType == InsuranceType.Health)
        {
            basePremium *= 1.1;

            if (insurance.HasPreviousClaims)
            {
                basePremium *= 1.4;
            }
        }

        if (insurance.Region == InsuranceRegion.HighRisk)
        {
            basePremium *= 1.6;
            if (insurance.HasPreviousClaims)
            {
                basePremium *= 1.4;
            }
        }

        _logger.LogInformation("Done!");


        insurance.PremiumAmount = basePremium;
        _insuranceRepository.Save(insurance);
    }
}

public interface IInsuranceRepository
{
    Insurance Find(int insuranceId);
    void Save(Insurance insurancee);
}
