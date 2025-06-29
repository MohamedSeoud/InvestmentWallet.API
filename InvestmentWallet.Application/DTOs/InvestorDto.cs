using InvestmentWallet.Domain.ValueObjects;

namespace InvestmentWallet.Application.DTOs;

public record InvestorDto(
    Guid Id,
    string Name,
    string Email,
    Money WalletBalance,
    DateTime CreatedAt
);