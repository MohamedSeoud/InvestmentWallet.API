using InvestmentWallet.Application.Commands;
using InvestmentWallet.Domain.Interfaces;
using MediatR;

namespace InvestmentWallet.Application.Handlers;

public class MakeInvestmentHandler : IRequestHandler<MakeInvestmentCommand, Unit>
{
    private readonly IInvestorRepository _investorRepository;
    private readonly IInvestmentOpportunityRepository _opportunityRepository;

    public MakeInvestmentHandler(
        IInvestorRepository investorRepository,
        IInvestmentOpportunityRepository opportunityRepository)
    {
        _investorRepository = investorRepository;
        _opportunityRepository = opportunityRepository;
    }

    public async Task<Unit> Handle(MakeInvestmentCommand request, CancellationToken cancellationToken)
    {
        var investor = await _investorRepository.GetByIdAsync(request.InvestorId, cancellationToken);
        if (investor == null)
            throw new ArgumentException("Investor not found");

        var opportunity = await _opportunityRepository.GetByIdAsync(request.OpportunityId, cancellationToken);
        if (opportunity == null)
            throw new ArgumentException("Investment opportunity not found");

        await _investorRepository.InvestAsync(request.InvestorId,opportunity, request.Amount, cancellationToken);

        return Unit.Value;
    }
}