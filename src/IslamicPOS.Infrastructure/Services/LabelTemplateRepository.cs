using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Core.Barcoding.Models;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services
{
    public class LabelTemplateRepository : ILabelTemplateRepository
    {
        private readonly ApplicationDbContext _context;

        public LabelTemplateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LabelTemplate>> GetAllTemplatesAsync()
        {
            return await _context.LabelTemplates
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<LabelTemplate> GetTemplateByIdAsync(string id)
        {
            return await _context.LabelTemplates
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<LabelTemplate> CreateTemplateAsync(LabelTemplate template)
        {
            template.Id = Guid.NewGuid().ToString();
            _context.LabelTemplates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task<LabelTemplate> UpdateTemplateAsync(string id, LabelTemplate template)
        {
            var existing = await GetTemplateByIdAsync(id);
            if (existing == null)
                return null;

            existing.Name = template.Name;
            existing.Description = template.Description;
            existing.Size = template.Size;
            existing.Layout = template.Layout;
            existing.Styling = template.Styling;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteTemplateAsync(string id)
        {
            var template = await GetTemplateByIdAsync(id);
            if (template == null)
                return false;

            _context.LabelTemplates.Remove(template);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LabelTemplate> GetDefaultTemplateAsync()
        {
            return await _context.LabelTemplates
                .FirstOrDefaultAsync(t => t.IsDefault);
        }

        public async Task SetDefaultTemplateAsync(string id)
        {
            var templates = await _context.LabelTemplates.ToListAsync();
            foreach (var template in templates)
            {
                template.IsDefault = template.Id == id;
            }
            await _context.SaveChangesAsync();
        }
    }
}