namespace InvestmentWallet.Application.DTOs;

public record InvestmentDto(
    Guid Id,
    string OpportunityName,
    decimal Amount,
    DateTime InvestmentDate
);