using Moq;

public class LoanProcessorTest
{
    [Fact]
    public void Should_get_positive_decision_when_has_no_existing_debts_and_has_enough_income()
    {
        ApplicationData applicationData = new ApplicationData()
            { Age = 20, CreditScore = GOOD, Income = 15000, Id = 3 };
        var loanAmountCalculator = new Mock<ILoanAmountCalculator>();
        loanAmountCalculator.Setup(x => x.CalculateMaximumLoanAmount(applicationData)).Returns(MAX_LOAN);
        var debtVerificationService = new Mock<IDebtVerificationService>();
        debtVerificationService.Setup(x => x.CheckExistingDebts(applicationData.Id)).Returns(false);
        var loanDecision = new Mock<ILoanDecision>();

        var sut = new LoanProcessor(loanAmountCalculator.Object, debtVerificationService.Object, loanDecision.Object);
        sut.ProcessApplication(applicationData, 10000);

        loanDecision.Verify(x => x.Accept(), Times.Once);
        loanDecision.VerifyNoOtherCalls();
    }

    private const decimal MAX_LOAN = 50000;

    private const int GOOD = 800;
}

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