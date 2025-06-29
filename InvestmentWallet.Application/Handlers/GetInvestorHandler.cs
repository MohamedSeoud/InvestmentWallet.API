using InvestmentWallet.Application.DTOs;
using InvestmentWallet.Application.Queries;
using InvestmentWallet.Domain.Interfaces;
using MediatR;

namespace InvestmentWallet.Application.Handlers;

public class GetInvestorHandler : IRequestHandler<GetInvestorQuery, InvestorDto?>
{
    private readonly IInvestorRepository _investorRepository;

    public GetInvestorHandler(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }

    public async Task<InvestorDto?> Handle(GetInvestorQuery request, CancellationToken cancellationToken)
    {
        var investor = await _investorRepository.GetByIdAsync(request.InvestorId, cancellationToken);

        return investor == null ? null : new InvestorDto(
            investor.Id,
            investor.Name,
            investor.Email,
            investor.WalletBalance,
            investor.CreatedAt
        );
    }
}