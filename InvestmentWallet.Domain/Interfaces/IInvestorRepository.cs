using InvestmentWallet.Domain.Entities;

namespace InvestmentWallet.Domain.Interfaces;

public interface IInvestorRepository
{
    Task<Investor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Investor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Investor> AddAsync(Investor investor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Investor investor, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task FundWalletAsync(Guid investorId, decimal amount, CancellationToken cancellationToken = default);
    Task InvestAsync(Guid investorId, InvestmentOpportunity opportunity, decimal amount, CancellationToken cancellationToken = default);


}