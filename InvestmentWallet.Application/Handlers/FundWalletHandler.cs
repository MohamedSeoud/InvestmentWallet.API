using InvestmentWallet.Application.Commands;
using InvestmentWallet.Domain.Interfaces;
using MediatR;

namespace InvestmentWallet.Application.Handlers;

public class FundWalletHandler : IRequestHandler<FundWalletCommand, Unit>
{
    private readonly IInvestorRepository _investorRepository;

    public FundWalletHandler(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }

    public async Task<Unit> Handle(FundWalletCommand request, CancellationToken cancellationToken)
    {
        var investor = await _investorRepository.GetByIdAsync(request.InvestorId, cancellationToken);
        if (investor == null)
            throw new ArgumentException("Investor not found");

        await _investorRepository.FundWalletAsync(request.InvestorId,request.Amount, cancellationToken);

        return Unit.Value;
    }
}
