public class LoanProcessor
{
    private readonly ILoanAmountCalculator _loanAmountCalculator;
    private readonly IDebtVerificationService _debtVerificationService;
    private readonly ILoanDecision _decision;
    private IApplicationRepository _applicationDataRepository;

    public LoanProcessor(ILoanAmountCalculator loanAmountCalculator, IDebtVerificationService debtVerificationService,
        ILoanDecision decision, IApplicationRepository applicationDataRepository)
    {
        _loanAmountCalculator = loanAmountCalculator;
        _debtVerificationService = debtVerificationService;
        _decision = decision;
        _applicationDataRepository = applicationDataRepository;
    }

    public void ProcessApplication(int applicationId, decimal requestedLoanAmount)
    {
        bool hasExistingDebts = _debtVerificationService.CheckExistingDebts(applicationId);
        ApplicationData applicationData = _applicationDataRepository.GetApplicationData(applicationId);
        decimal maximumLoanAmount = _loanAmountCalculator.CalculateMaximumLoanAmount(applicationData);

        if (applicationData.Age >= 18 && applicationData.Income >= 2000 && applicationData.CreditScore >= 600 &&
            !hasExistingDebts && requestedLoanAmount < maximumLoanAmount)
        {
                _decision.Accept();
        }
        else
        {
            _decision.Reject();
        }
    }
}

public interface IApplicationRepository
{
    ApplicationData GetApplicationData(int applicationId);
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