using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces;
using InvestmentWallet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvestmentWallet.Infrastructure.Repositories;

public class InvestmentOpportunityRepository : IInvestmentOpportunityRepository
{
    private readonly InvestmentWalletDbContext _context;

    public InvestmentOpportunityRepository(InvestmentWalletDbContext context)
    {
        _context = context;
    }

    public async Task<List<InvestmentOpportunity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.InvestmentOpportunities.ToListAsync(cancellationToken);
    }

    public async Task<InvestmentOpportunity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.InvestmentOpportunities.FindAsync(new object[] { id }, cancellationToken);
    }
}