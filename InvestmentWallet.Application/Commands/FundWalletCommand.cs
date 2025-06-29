using MediatR;

namespace InvestmentWallet.Application.Commands;

public record FundWalletCommand(
    Guid InvestorId,
    decimal Amount
) : IRequest<Unit>;