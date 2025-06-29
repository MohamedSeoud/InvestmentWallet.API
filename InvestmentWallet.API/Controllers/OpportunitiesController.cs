using InvestmentWallet.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentWallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpportunitiesController : ControllerBase
{
    private readonly IInvestmentOpportunityRepository _repository;

    public OpportunitiesController(IInvestmentOpportunityRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetOpportunities()
    {
        var opportunities = await _repository.GetAllAsync();
        return Ok(opportunities);
    }
}