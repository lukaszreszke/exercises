namespace ClaimObjectMother;

public class Claim
{
    public int ClaimId { get; private set; }
    public string PolicyNumber { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DateOfLoss { get; private set; }
    public bool IsApproved { get; private set; }

    public Claim(int claimId, string policyNumber, decimal amount, DateTime dateOfLoss)
    {
        ClaimId = claimId;
        PolicyNumber = policyNumber;
        Amount = amount;
        DateOfLoss = dateOfLoss;
        IsApproved = false;
    }

    public void ApproveClaim()
    {
        if (!IsApproved)
        {
            if (Amount <= 5000.00m && (DateTime.Now - DateOfLoss).Days <= 30)
            {
                IsApproved = true;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}


