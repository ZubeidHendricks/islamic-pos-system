using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Core.Barcoding.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LabelTemplateController : ControllerBase
    {
        private readonly ILabelTemplateRepository _templateRepository;

        public LabelTemplateController(ILabelTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<LabelTemplate>>> GetAllTemplates()
        {
            var templates = await _templateRepository.GetAllTemplatesAsync();
            return Ok(templates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LabelTemplate>> GetTemplate(string id)
        {
            var template = await _templateRepository.GetTemplateByIdAsync(id);
            if (template == null)
                return NotFound();
            return Ok(template);
        }

        [HttpPost]
        public async Task<ActionResult<LabelTemplate>> CreateTemplate(LabelTemplate template)
        {
            var created = await _templateRepository.CreateTemplateAsync(template);
            return CreatedAtAction(nameof(GetTemplate), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LabelTemplate>> UpdateTemplate(string id, LabelTemplate template)
        {
            var updated = await _templateRepository.UpdateTemplateAsync(id, template);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemplate(string id)
        {
            var result = await _templateRepository.DeleteTemplateAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("default")]
        public async Task<ActionResult<LabelTemplate>> GetDefaultTemplate()
        {
            var template = await _templateRepository.GetDefaultTemplateAsync();
            if (template == null)
                return NotFound();
            return Ok(template);
        }

        [HttpPost("{id}/set-default")]
        public async Task<ActionResult> SetDefaultTemplate(string id)
        {
            await _templateRepository.SetDefaultTemplateAsync(id);
            return Ok();
        }
    }
}