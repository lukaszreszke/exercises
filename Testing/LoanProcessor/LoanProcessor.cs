public class LoanProcessor
{
    private readonly ILoanAmountCalculator _loanAmountCalculator;
    private readonly IDebtVerificationService _debtVerificationService;
    private readonly ILoanDecision _decision;

    public LoanProcessor(ILoanAmountCalculator loanAmountCalculator, IDebtVerificationService debtVerificationService,
        ILoanDecision decision)
    {
        _loanAmountCalculator = loanAmountCalculator;
        _debtVerificationService = debtVerificationService;
        _decision = decision;
    }

    public void ProcessApplication(ApplicationData applicationData, decimal requestedLoanAmount)
    {
        bool hasExistingDebts = _debtVerificationService.CheckExistingDebts(applicationData.Id);

        if (applicationData.Age >= 18 && applicationData.Income >= 2000 && applicationData.CreditScore >= 600 &&
            !hasExistingDebts)
        {
            decimal maximumLoanAmount = _loanAmountCalculator.CalculateMaximumLoanAmount(applicationData);

            if (requestedLoanAmount <= maximumLoanAmount)
            {
                _decision.Accept();
            }
            else
            {
                _decision.Reject();
            }
        }
        else
        {
            _decision.Reject();
        }
    }
}

public interface ILoanDecision
{
    void Accept();
    void Reject();
}


public interface ILoanAmountCalculator
{
    decimal CalculateMaximumLoanAmount(ApplicationData applicationData);
}

public class LoanAmountCalculator : ILoanAmountCalculator
{
    private readonly decimal _maximumLoanPercentage;

    public LoanAmountCalculator(decimal maximumLoanPercentage)
    {
        _maximumLoanPercentage = maximumLoanPercentage;
    }

    public decimal CalculateMaximumLoanAmount(ApplicationData applicationData)
    {
        decimal maximumLoanAmount = applicationData.Income * _maximumLoanPercentage;
        return maximumLoanAmount;
    }
}

public class ApplicationData
{
    public int Id { get; set; }
    public int Age { get; set; }
    public decimal Income { get; set; }
    public int CreditScore { get; set; }
}

public interface IDebtVerificationService
{
    bool CheckExistingDebts(int applicantId);
}