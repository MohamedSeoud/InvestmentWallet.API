using MediatR;

namespace InvestmentWallet.Application.Commands;

public record MakeInvestmentCommand(
    Guid InvestorId,
    Guid OpportunityId,
    decimal Amount
) : IRequest<Unit>;