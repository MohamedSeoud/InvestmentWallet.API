namespace InvestmentWallet.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));

        Amount = Math.Round(amount, 2);
    }

    public static Money Zero() => new(0);

    public Money Add(decimal amount) => new(Amount + amount);

    public Money Subtract(decimal amount) => new(Amount - amount);

    public static implicit operator decimal(Money money) => money.Amount;
}