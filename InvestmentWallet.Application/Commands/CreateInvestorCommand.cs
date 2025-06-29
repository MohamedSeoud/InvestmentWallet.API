using InvestmentWallet.Application.DTOs;
using MediatR;

namespace InvestmentWallet.Application.Commands;

public record CreateInvestorCommand(
    string Name,
    string Email
) : IRequest<InvestorDto>;