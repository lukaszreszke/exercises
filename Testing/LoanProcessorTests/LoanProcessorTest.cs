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
        var applicationRepository = new Mock<IApplicationRepository>();
        applicationRepository.Setup(x => x.GetApplicationData(applicationData.Id)).Returns(applicationData);

        var sut = new LoanProcessor(loanAmountCalculator.Object, debtVerificationService.Object, loanDecision.Object, applicationRepository.Object);
        sut.ProcessApplication(3, 10000);

        loanDecision.Verify(x => x.Accept(), Times.Once);
        loanDecision.VerifyNoOtherCalls();
    }

    private const decimal MAX_LOAN = 50000;

    private const int GOOD = 800;
}