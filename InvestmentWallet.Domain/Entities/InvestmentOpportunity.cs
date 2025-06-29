namespace InvestmentWallet.Domain.Entities;

public class InvestmentOpportunity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal MinimumInvestment { get; private set; }

    private InvestmentOpportunity() { } 

    public InvestmentOpportunity(string name, decimal minimumInvestment)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        MinimumInvestment = minimumInvestment;
    }

    public static List<InvestmentOpportunity> GetDefaultOpportunities()
    {
        return new List<InvestmentOpportunity>
        {
            new("Real Estate Fund", 1000m),
            new("Tech Growth Fund", 500m),
            new("SME Sukuk", 250m)
        };
    }
}