using ContractManagementRefactor;
using Moq;

namespace ContractManagementRefactorTests
{
    public class ContractTests
    {
        [Fact]
        public void add_change_adds_proposal_to_list()
        {
            // Arrange
            var contract = new Contract();
            var proposalMock = new Mock<Proposal>();

            // Act
            contract.AddChange(proposalMock.Object);

            // Assert
            Assert.Contains(proposalMock.Object, contract.Changes);
        }

        [Fact]
        public void accept_proposal_accepted_when_proposal_exists()
        {
            // Arrange
            var contract = new Contract();
            var proposalMock = new Mock<Proposal>();
            contract.AddChange(proposalMock.Object);

            // Act
            contract.Accept(proposalMock.Object);

            // Assert
            proposalMock.Verify(p => p.Accept(), Times.Once);
        }

        [Fact]
        public void reject_change_proposal_rejected_when_proposal_exists()
        {
            // Arrange
            var contract = new Contract();
            var proposalMock = new Mock<Proposal>();
            contract.AddChange(proposalMock.Object);

            // Act
            contract.RejectChange(proposalMock.Object);

            // Assert
            proposalMock.Verify(p => p.Reject(), Times.Once);
        }

        [Fact]
        public void activate_contract_sets_contract_status_to_active()
        {
            // Arrange
            var contract = new Contract();
            contract.CompleteNegotiations();

            // Act
            contract.ActivateContract();

            // Assert
            Assert.Equal(Contract.Status.Active, contract.ContractStatus);
        }

        [Fact]
        public void suspend_contract_sets_contract_status_to_suspended()
        {
            // Arrange
            var contract = new Contract();
            contract.CompleteNegotiations();
            contract.ActivateContract();

            // Act
            contract.SuspendContract();

            // Assert
            Assert.Equal(Contract.Status.Suspended, contract.ContractStatus);
        }

        [Fact]
        public void contract_status_is_negotiations_in_progress_when_contract_is_created()
        {
            // Arrange
            var contract = new Contract();

            // Assert
            Assert.Equal(Contract.Status.NegotiationsInProgress, contract.ContractStatus);
        }

        [Fact]
        public void terminate_contract_sets_contract_status_to_terminated()
        {
            // Arrange
            var contract = new Contract();

            // Act
            contract.TerminateContract();

            // Assert
            Assert.Equal(Contract.Status.Terminated, contract.ContractStatus);
        }

        [Fact]
        public void accept_throws_argument_exception_when_proposal_does_not_exist()
        {
            // Arrange
            var contract = new Contract();
            var proposalMock = new Mock<Proposal>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => contract.Accept(proposalMock.Object));
        }

        [Fact]
        public void reject_change_throws_argument_exception_when_proposal_does_not_exist()
        {
            // Arrange
            var contract = new Contract();
            var proposalMock = new Mock<Proposal>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => contract.RejectChange(proposalMock.Object));
        }
    }
}