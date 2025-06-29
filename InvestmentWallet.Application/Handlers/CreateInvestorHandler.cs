using InvestmentWallet.Application.Commands;
using InvestmentWallet.Application.DTOs;
using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces;
using MediatR;

namespace InvestmentWallet.Application.Handlers;

public class CreateInvestorHandler : IRequestHandler<CreateInvestorCommand, InvestorDto>
{
    private readonly IInvestorRepository _investorRepository;

    public CreateInvestorHandler(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }

    public async Task<InvestorDto> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
    {
        var existingInvestor = await _investorRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingInvestor != null)
            throw new InvalidOperationException("Investor with this email already exists");

        var investor = new Investor(request.Name, request.Email);
        await _investorRepository.AddAsync(investor, cancellationToken);

        return new InvestorDto(
            investor.Id,
            investor.Name,
            investor.Email,
            investor.WalletBalance,
            investor.CreatedAt
        );
    }
}
