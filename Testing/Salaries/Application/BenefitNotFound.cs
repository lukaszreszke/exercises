namespace Salaries.Application;

public class BenefitNotFound : Exception
{
    public int BenefitId { get; }

    public BenefitNotFound(int benefitId)
    {
        BenefitId = benefitId;
    }
}