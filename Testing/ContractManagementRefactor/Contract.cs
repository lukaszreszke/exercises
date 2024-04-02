namespace ContractManagementRefactor
{
    public class Contract
    {
        public enum Status
        {
            NegotiationsInProgress,
            NegotiationsCompleted,
            NegotiationsDropped,
            Active,
            Suspended,
            Terminated
        }

        public Contract()
        {
            ContractStatus = Status.NegotiationsInProgress;
        }

        public List<Proposal> Changes = new List<Proposal>();

        public Status ContractStatus { get; private set; }

        public void AddChange(Proposal proposal)
        {
            Changes.Add(proposal);
        }

        public void Accept(Proposal proposal)
        {
            if (Changes.Contains(proposal))
            {
                proposal.Accept();
            }
            else
            {
                throw new ArgumentException("Proposal does not exist in the list of changes.");
            }
        }

        public void RejectChange(Proposal proposal)
        {
            if (Changes.Contains(proposal))
            {
                proposal.Reject();
            }
            else
            {
                throw new ArgumentException("Proposal does not exist in the list of changes.");
            }
        }

        public void CompleteNegotiations()
        {
            if (ContractStatus == Status.NegotiationsInProgress)
                ContractStatus = Status.NegotiationsCompleted;
        }

        public bool ActivateContract()
        {
            if (ContractStatus == Status.NegotiationsCompleted)
            {
                ContractStatus = Status.Active;
                return true;
            }

            return false;
        }

        public void SuspendContract()
        {
            if (ContractStatus == Status.Active)
            {
                ContractStatus = Status.Suspended;
            }
            else
            {
                throw new InvalidOperationException("Cannot suspend non-active contract");
            }
        }

        public void TerminateContract()
        {
            ContractStatus = Status.Terminated;
        }

        public bool DropNegotiations()
        {
            if (ContractStatus == Status.NegotiationsInProgress)
            {
                ContractStatus = Status.NegotiationsDropped;
                return true;
            }

            return false;
        }
    }
}