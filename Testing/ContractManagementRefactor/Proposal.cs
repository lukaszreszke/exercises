namespace ContractManagementRefactor
{
    public class Proposal
    {
        public enum Status
        {
            AcceptedByOneSide,
            AcceptedByTwoSides,
            Rejected
        }

        private Status status;
        private byte[] attachment;

        public virtual void Accept()
        {
            status = Status.AcceptedByOneSide;
        }

        public virtual void Reject()
        {
            status = Status.Rejected;
        }
    }
}