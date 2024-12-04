using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Barcoding.Models;

namespace IslamicPOS.Core.Barcoding.Interfaces
{
    public interface ILabelTemplateRepository
    {
        Task<List<LabelTemplate>> GetAllTemplatesAsync();
        Task<LabelTemplate> GetTemplateByIdAsync(string id);
        Task<LabelTemplate> CreateTemplateAsync(LabelTemplate template);
        Task<LabelTemplate> UpdateTemplateAsync(string id, LabelTemplate template);
        Task<bool> DeleteTemplateAsync(string id);
        Task<LabelTemplate> GetDefaultTemplateAsync();
        Task SetDefaultTemplateAsync(string id);
    }
}