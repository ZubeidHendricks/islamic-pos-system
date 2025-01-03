using IslamicPOS.Core.DTOs;
using IslamicPOS.Core.Models.IslamicFinance;
using IslamicPOS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfitSharingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProfitSharingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfitSharingDto>>> GetProfitSharings()
    {
        var profitSharings = await _context.ProfitSharings
            .Include(p => p.DistributionDetails)
            .ThenInclude(d => d.Partner)
            .Select(p => new ProfitSharingDto
            {
                Id = p.Id,
                TotalAmount = p.TotalAmount,
                DistributionDate = p.DistributionDate,
                Description = p.Description,
                Period = p.Period,
                IsDistributed = p.IsDistributed,
                DistributedAt = p.DistributedAt,
                DistributionDetails = p.DistributionDetails.Select(d => new ProfitDistributionDetailDto
                {
                    Id = d.Id,
                    PartnerId = d.PartnerId,
                    PartnerName = d.Partner.Name,
                    Amount = d.Amount,
                    Percentage = d.Percentage,
                    IsPaid = d.IsPaid,
                    PaymentDate = d.PaymentDate,
                    PaymentReference = d.PaymentReference,
                    Notes = d.Notes
                }).ToList()
            })
            .ToListAsync();

        return Ok(profitSharings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfitSharingDto>> GetProfitSharing(Guid id)
    {
        var profitSharing = await _context.ProfitSharings
            .Include(p => p.DistributionDetails)
            .ThenInclude(d => d.Partner)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (profitSharing == null)
        {
            return NotFound();
        }

        var dto = new ProfitSharingDto
        {
            Id = profitSharing.Id,
            TotalAmount = profitSharing.TotalAmount,
            DistributionDate = profitSharing.DistributionDate,
            Description = profitSharing.Description,
            Period = profitSharing.Period,
            IsDistributed = profitSharing.IsDistributed,
            DistributedAt = profitSharing.DistributedAt,
            DistributionDetails = profitSharing.DistributionDetails.Select(d => new ProfitDistributionDetailDto
            {
                Id = d.Id,
                PartnerId = d.PartnerId,
                PartnerName = d.Partner.Name,
                Amount = d.Amount,
                Percentage = d.Percentage,
                IsPaid = d.IsPaid,
                PaymentDate = d.PaymentDate,
                PaymentReference = d.PaymentReference,
                Notes = d.Notes
            }).ToList()
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ProfitSharingDto>> CreateProfitSharing(ProfitSharingDto profitSharingDto)
    {
        var profitSharing = new ProfitSharing
        {
            TotalAmount = profitSharingDto.TotalAmount,
            DistributionDate = profitSharingDto.DistributionDate,
            Description = profitSharingDto.Description,
            Period = profitSharingDto.Period,
            IsDistributed = false,
            DistributionDetails = profitSharingDto.DistributionDetails.Select(d => new ProfitDistributionDetail
            {
                PartnerId = d.PartnerId,
                Amount = d.Amount,
                Percentage = d.Percentage,
                IsPaid = false
            }).ToList()
        };

        _context.ProfitSharings.Add(profitSharing);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProfitSharing), new { id = profitSharing.Id }, profitSharingDto);
    }

    [HttpPost("{id}/distribute")]
    public async Task<IActionResult> DistributeProfits(Guid id)
    {
        var profitSharing = await _context.ProfitSharings.FindAsync(id);

        if (profitSharing == null)
        {
            return NotFound();
        }

        if (profitSharing.IsDistributed)
        {
            return BadRequest("Profits have already been distributed.");
        }

        profitSharing.IsDistributed = true;
        profitSharing.DistributedAt = DateTime.UtcNow;
        profitSharing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}