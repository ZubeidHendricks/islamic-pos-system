using System.Threading.Tasks;
using IslamicPOS.Core.Barcoding.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeGenerationService _barcodeGenerationService;

        public BarcodeController(IBarcodeGenerationService barcodeGenerationService)
        {
            _barcodeGenerationService = barcodeGenerationService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateBarcode([FromBody] BarcodeGenerationRequest request)
        {
            var result = await _barcodeGenerationService.GenerateBarcodeDocument(request);
            return File(result.Content, GetContentType(request.Format), $"barcode.{GetFileExtension(request.Format)}");
        }

        [HttpPost("label")]
        public async Task<IActionResult> GenerateLabel([FromBody] BarcodeLabelRequest request)
        {
            var result = await _barcodeGenerationService.GenerateBarcodeLabel(request);
            return File(result, "application/pdf", "barcode-label.pdf");
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> GenerateBulkBarcodes([FromBody] BulkBarcodeRequest request)
        {
            var result = await _barcodeGenerationService.GenerateBulkBarcodes(request);
            return File(result, "application/pdf", "barcodes.pdf");
        }

        private string GetContentType(PrintFormat format)
        {
            return format switch
            {
                PrintFormat.PDF => "application/pdf",
                PrintFormat.PNG => "image/png",
                PrintFormat.SVG => "image/svg+xml",
                _ => "application/pdf"
            };
        }

        private string GetFileExtension(PrintFormat format)
        {
            return format switch
            {
                PrintFormat.PDF => "pdf",
                PrintFormat.PNG => "png",
                PrintFormat.SVG => "svg",
                _ => "pdf"
            };
        }
    }
}