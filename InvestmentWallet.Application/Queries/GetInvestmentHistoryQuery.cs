using InvestmentWallet.Application.DTOs;
using MediatR;

namespace InvestmentWallet.Application.Queries;

public record GetInvestmentHistoryQuery(Guid InvestorId) : IRequest<List<InvestmentDto>>;