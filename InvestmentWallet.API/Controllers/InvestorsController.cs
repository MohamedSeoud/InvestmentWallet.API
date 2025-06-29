using InvestmentWallet.Application.Commands;
using InvestmentWallet.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentWallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvestorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvestor([FromBody] CreateInvestorCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetInvestor), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvestor(Guid id)
    {
        var result = await _mediator.Send(new GetInvestorQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("{id}/fund")]
    public async Task<IActionResult> FundWallet(Guid id, [FromBody] FundWalletRequest request)
    {
        try
        {
            await _mediator.Send(new FundWalletCommand(id, request.Amount));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{id}/invest")]
    public async Task<IActionResult> MakeInvestment(Guid id, [FromBody] MakeInvestmentRequest request)
    {
        try
        {
            await _mediator.Send(new MakeInvestmentCommand(id, request.OpportunityId, request.Amount));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}/investments")]
    public async Task<IActionResult> GetInvestmentHistory(Guid id)
    {
        var result = await _mediator.Send(new GetInvestmentHistoryQuery(id));
        return Ok(result);
    }
}

public record FundWalletRequest(decimal Amount);
public record MakeInvestmentRequest(Guid OpportunityId, decimal Amount);