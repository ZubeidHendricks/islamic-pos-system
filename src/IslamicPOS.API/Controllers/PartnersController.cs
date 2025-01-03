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
    public async Task<ActionResult<IEnumerable<Partner>>> GetPartners()
    {
        return await _context.Partners.Where(p => !p.IsDeleted).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Partner>> GetPartner(Guid id)
    {
        var partner = await _context.Partners.FindAsync(id);

        if (partner == null || partner.IsDeleted)
            return NotFound();

        return partner;
    }

    [HttpPost]
    public async Task<ActionResult<Partner>> CreatePartner(Partner partner)
    {
        _context.Partners.Add(partner);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPartner), new { id = partner.Id }, partner);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartner(Guid id, Partner partner)
    {
        if (id != partner.Id)
            return BadRequest();

        var existingPartner = await _context.Partners.FindAsync(id);
        if (existingPartner == null || existingPartner.IsDeleted)
            return NotFound();

        existingPartner.Name = partner.Name;
        existingPartner.ContactNumber = partner.ContactNumber;
        existingPartner.Email = partner.Email;
        existingPartner.SharePercentage = partner.SharePercentage;
        existingPartner.InvestmentAmount = partner.InvestmentAmount;
        existingPartner.IsActive = partner.IsActive;
        existingPartner.Notes = partner.Notes;
        existingPartner.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePartner(Guid id)
    {
        var partner = await _context.Partners.FindAsync(id);
        if (partner == null || partner.IsDeleted)
            return NotFound();

        partner.IsDeleted = true;
        partner.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}