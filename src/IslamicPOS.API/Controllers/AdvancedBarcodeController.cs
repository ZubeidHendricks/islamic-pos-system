using System;
using System.Threading.Tasks;
using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Core.Barcoding.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdvancedBarcodeController : ControllerBase
    {
        private readonly IAdvancedBarcodeService _advancedBarcodeService;
        private readonly ILabelTemplateRepository _templateRepository;

        public AdvancedBarcodeController(
            IAdvancedBarcodeService advancedBarcodeService,
            ILabelTemplateRepository templateRepository)
        {
            _advancedBarcodeService = advancedBarcodeService;
            _templateRepository = templateRepository;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateAdvancedLabel(
            [FromForm] AdvancedLabelRequest request)
        {
            try
            {
                var template = await _templateRepository.GetTemplateByIdAsync(request.TemplateId)
                    ?? await _templateRepository.GetDefaultTemplateAsync();

                var options = new AdvancedPrintingOptions
                {
                    Template = new TemplateOptions
                    {
                        TemplateId = template.Id,
                        IncludePrice = request.IncludePrice,
                        IncludeDescription = request.IncludeDescription,
                        IncludeDate = request.IncludeDate,
                        CompanyName = request.CompanyName,
                        CustomText = request.CustomText
                    }
                };

                // Handle logo if provided
                if (request.Logo != null)
                {
                    using var ms = new MemoryStream();
                    await request.Logo.CopyToAsync(ms);
                    options.Logo = new LogoOptions
                    {
                        LogoData = ms.ToArray(),
                        Position = request.LogoPosition,
                        MaxWidth = request.LogoMaxWidth ?? 100,
                        MaxHeight = request.LogoMaxHeight ?? 100
                    };
                }

                var barcodeData = new BarcodeData
                {
                    Content = request.Content,
                    Type = request.BarcodeType,
                    Width = template.Layout.BarcodeWidth,
                    Height = template.Layout.BarcodeHeight
                };

                var result = await _advancedBarcodeService.GenerateAdvancedLabel(
                    barcodeData, options);

                return File(result, "application/pdf", "advanced-label.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public class AdvancedLabelRequest
        {
            public string Content { get; set; }
            public BarcodeType BarcodeType { get; set; }
            public string TemplateId { get; set; }
            public bool IncludePrice { get; set; }
            public bool IncludeDescription { get; set; }
            public bool IncludeDate { get; set; }
            public string CompanyName { get; set; }
            public string CustomText { get; set; }
            public IFormFile Logo { get; set; }
            public LogoPosition LogoPosition { get; set; }
            public int? LogoMaxWidth { get; set; }
            public int? LogoMaxHeight { get; set; }
        }
    }
}