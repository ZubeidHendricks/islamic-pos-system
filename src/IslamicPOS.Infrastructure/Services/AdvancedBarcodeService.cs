using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using IslamicPOS.Core.Barcoding.Models;

namespace IslamicPOS.Infrastructure.Services
{
    public class AdvancedBarcodeService
    {
        private readonly IBarcodeService _barcodeService;

        public AdvancedBarcodeService(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        public async Task<byte[]> GenerateAdvancedLabel(BarcodeData barcodeData, AdvancedPrintingOptions options)
        {
            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            // Add logo if specified
            if (options.Logo != null)
            {
                var logoImage = GetLogoImage(options.Logo);
                if (logoImage != null)
                {
                    AddLogo(document, logoImage, options.Logo);
                }
            }

            // Generate and add barcode
            var barcodeImage = await _barcodeService.GenerateBarcode(barcodeData);
            var image = new Image(ImageDataFactory.Create(barcodeImage));

            // Apply template styling if specified
            if (options.Template != null)
            {
                ApplyTemplateStyles(document, options.Template);
            }

            document.Add(image);

            // Add additional content based on template options
            if (options.Template?.IncludePrice == true)
            {
                document.Add(new Paragraph("Price: $XX.XX"));
            }

            if (options.Template?.IncludeDate == true)
            {
                document.Add(new Paragraph($"Date: {DateTime.Now:d}"));
            }

            if (!string.IsNullOrEmpty(options.Template?.CustomText))
            {
                document.Add(new Paragraph(options.Template.CustomText));
            }

            document.Close();
            return ms.ToArray();
        }

        private ImageData GetLogoImage(LogoOptions logoOptions)
        {
            if (logoOptions.LogoData != null)
            {
                return ImageDataFactory.Create(logoOptions.LogoData);
            }

            if (!string.IsNullOrEmpty(logoOptions.LogoUrl))
            {
                // Download logo from URL
                using var webClient = new System.Net.WebClient();
                var logoData = webClient.DownloadData(logoOptions.LogoUrl);
                return ImageDataFactory.Create(logoData);
            }

            return null;
        }

        private void AddLogo(Document document, ImageData logoImage, LogoOptions options)
        {
            var logo = new Image(logoImage);

            // Set size constraints
            if (options.MaxWidth > 0)
                logo.SetMaxWidth(options.MaxWidth);
            if (options.MaxHeight > 0)
                logo.SetMaxHeight(options.MaxHeight);

            // Set opacity
            if (options.Opacity < 1.0f)
                logo.SetOpacity(options.Opacity);

            // Position logo according to specified position
            switch (options.Position)
            {
                case LogoPosition.Background:
                    logo.SetFixedPosition(0, 0);
                    break;
                case LogoPosition.TopRight:
                    logo.SetFixedPosition(document.GetPageEffectiveArea(PageSize.A4).GetWidth() - options.MaxWidth, 
                        document.GetPageEffectiveArea(PageSize.A4).GetHeight() - options.MaxHeight);
                    break;
                // Add other position cases as needed
                default:
                    document.Add(logo);
                    break;
            }
        }

        private void ApplyTemplateStyles(Document document, TemplateOptions template)
        {
            if (template.FontSettings != null)
            {
                // Apply font settings
                // Note: This would require additional iText configuration
            }

            if (!string.IsNullOrEmpty(template.CompanyName))
            {
                var companyHeader = new Paragraph(template.CompanyName)
                    .SetFontSize(14)
                    .SetBold();
                document.Add(companyHeader);
            }
        }
    }
}