using InvestmentWallet.Application.Commands;
using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces;
using InvestmentWallet.Domain.ValueObjects;
using InvestmentWallet.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class InvestorRepository : IInvestorRepository
{
    private readonly InvestmentWalletDbContext _context;

    public InvestorRepository(InvestmentWalletDbContext context)
    {
        _context = context;
    }

    public async Task<Investor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Investors
            .Include(i => i.Investments)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<Investor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.ToLowerInvariant();
        return await _context.Investors.AsNoTracking()
            .FirstOrDefaultAsync(i => i.Email == new Email(normalizedEmail), cancellationToken);
    }

    public async Task<Investor> AddAsync(Investor investor, CancellationToken cancellationToken = default)
    {
        _context.Investors.Add(investor);
        await _context.SaveChangesAsync(cancellationToken);
        return investor;
    }

    public async Task UpdateAsync(Investor investor, CancellationToken cancellationToken = default)
    {
        _context.Investors.Update(investor);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task FundWalletAsync(Guid investorId, decimal amount, CancellationToken cancellationToken = default)
    {
        try
        {
            var investor = await _context.Investors
                .FirstOrDefaultAsync(i => i.Id == investorId, cancellationToken);

            if (investor == null)
                throw new InvalidOperationException("Investor not found");

            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            investor.FundWallet(+amount);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task InvestAsync(Guid investorId, InvestmentOpportunity opportunity, decimal amount, CancellationToken cancellationToken = default)
    {
        if (opportunity == null)
            throw new ArgumentNullException(nameof(opportunity));

        if (amount <= 0)
            throw new ArgumentException("Investment amount must be positive", nameof(amount));

        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var investor = await _context.Investors
                .FirstOrDefaultAsync(i => i.Id == investorId, cancellationToken);

            if (investor == null)
                throw new InvalidOperationException("Investor not found");

            if (amount < opportunity.MinimumInvestment)
                throw new InvalidOperationException($"Minimum investment is {opportunity.MinimumInvestment}");

            if (investor.WalletBalance.Amount < amount)
                throw new InvalidOperationException("Insufficient wallet balance");

            investor.FundWallet(-amount);

            var investment = new Investment(investorId, opportunity.Id, opportunity.Name, amount);
            _context.Investments.Add(investment);  

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Investment failed. Please try again.", ex);
        }
    }
}
