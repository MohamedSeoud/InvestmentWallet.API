using InvestmentWallet.Domain.Entities;

namespace InvestmentWallet.Domain.Interfaces;

public interface IInvestmentOpportunityRepository
{
    Task<List<InvestmentOpportunity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InvestmentOpportunity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}