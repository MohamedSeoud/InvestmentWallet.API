using InvestmentWallet.Application.DTOs;
using MediatR;

namespace InvestmentWallet.Application.Queries;

public record GetInvestorQuery(Guid InvestorId) : IRequest<InvestorDto?>;