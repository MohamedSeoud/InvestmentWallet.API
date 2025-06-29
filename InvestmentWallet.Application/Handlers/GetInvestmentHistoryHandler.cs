using InvestmentWallet.Application.DTOs;
using InvestmentWallet.Application.Queries;
using InvestmentWallet.Domain.Interfaces;
using MediatR;

namespace InvestmentWallet.Application.Handlers;

public class GetInvestmentHistoryHandler : IRequestHandler<GetInvestmentHistoryQuery, List<InvestmentDto>>
{
    private readonly IInvestorRepository _investorRepository;

    public GetInvestmentHistoryHandler(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }

    public async Task<List<InvestmentDto>> Handle(GetInvestmentHistoryQuery request, CancellationToken cancellationToken)
    {
        var investor = await _investorRepository.GetByIdAsync(request.InvestorId, cancellationToken);
        if (investor == null)
            return new List<InvestmentDto>();

        return investor.Investments
            .Select(i => new InvestmentDto(i.Id, i.OpportunityName, i.Amount, i.InvestmentDate))
            .OrderByDescending(i => i.InvestmentDate)
            .ToList();
    }
}