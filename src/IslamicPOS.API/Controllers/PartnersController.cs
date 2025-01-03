using IslamicPOS.Core.DTOs;
using IslamicPOS.Core.Models.Financial;
using IslamicPOS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartnersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PartnersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartnerDto>>> GetPartners()
    {
        var partners = await _context.Partners
            .Select(p => new PartnerDto
            {
                Id = p.Id,
                Name = p.Name,
                ContactNumber = p.ContactNumber,
                Email = p.Email,
                SharePercentage = p.SharePercentage,
                IsActive = p.IsActive,
                Notes = p.Notes
            })
            .ToListAsync();

        return Ok(partners);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartnerDto>> GetPartner(Guid id)
    {
        var partner = await _context.Partners.FindAsync(id);

        if (partner == null)
        {
            return NotFound();
        }

        var dto = new PartnerDto
        {
            Id = partner.Id,
            Name = partner.Name,
            ContactNumber = partner.ContactNumber,
            Email = partner.Email,
            SharePercentage = partner.SharePercentage,
            IsActive = partner.IsActive,
            Notes = partner.Notes
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<PartnerDto>> CreatePartner(PartnerDto partnerDto)
    {
        var partner = new Partner
        {
            Name = partnerDto.Name,
            ContactNumber = partnerDto.ContactNumber,
            Email = partnerDto.Email,
            SharePercentage = partnerDto.SharePercentage,
            IsActive = partnerDto.IsActive,
            Notes = partnerDto.Notes
        };

        _context.Partners.Add(partner);
        await _context.SaveChangesAsync();

        partnerDto.Id = partner.Id;

        return CreatedAtAction(nameof(GetPartner), new { id = partner.Id }, partnerDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartner(Guid id, PartnerDto partnerDto)
    {
        var partner = await _context.Partners.FindAsync(id);

        if (partner == null)
        {
            return NotFound();
        }

        partner.Name = partnerDto.Name;
        partner.ContactNumber = partnerDto.ContactNumber;
        partner.Email = partnerDto.Email;
        partner.SharePercentage = partnerDto.SharePercentage;
        partner.IsActive = partnerDto.IsActive;
        partner.Notes = partnerDto.Notes;
        partner.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePartner(Guid id)
    {
        var partner = await _context.Partners.FindAsync(id);
        if (partner == null)
        {
            return NotFound();
        }

        _context.Partners.Remove(partner);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}