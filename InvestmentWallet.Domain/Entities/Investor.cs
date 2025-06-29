using InvestmentWallet.Domain.ValueObjects;

namespace InvestmentWallet.Domain.Entities;

public class Investor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Money WalletBalance { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<Investment> Investments { get; private set; } = new List<Investment>();

    private Investor() { }

    public Investor(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = new Email(email);
        WalletBalance = Money.Zero();
        CreatedAt = DateTime.UtcNow;
        Investments = new List<Investment>();
    }

    public void FundWallet(decimal amount)
    {
        if (amount == 0)
            return;

        WalletBalance = WalletBalance.Add(amount);
    }


}