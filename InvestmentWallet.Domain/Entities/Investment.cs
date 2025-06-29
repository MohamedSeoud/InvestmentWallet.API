namespace InvestmentWallet.Domain.Entities;

public class Investment
{
    public Guid Id { get; private set; }
    public Guid InvestorId { get; private set; }
    public Guid OpportunityId { get; private set; }
    public string OpportunityName { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime InvestmentDate { get; private set; }
    public Investor Investor { get; private set; } 

    
    private Investment() { }
    public Investment(Guid investorId, Guid opportunityId, string opportunityName, decimal amount)
    {
        Id = Guid.NewGuid();
        InvestorId = investorId;
        OpportunityId = opportunityId;
        OpportunityName = opportunityName ?? throw new ArgumentNullException(nameof(opportunityName));
        Amount = amount;
        InvestmentDate = DateTime.UtcNow;
    }
}