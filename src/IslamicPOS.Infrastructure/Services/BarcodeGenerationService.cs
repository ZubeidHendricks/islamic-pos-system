using System;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Core.Barcoding.Models;

namespace IslamicPOS.Infrastructure.Services
{
    public class BarcodeGenerationService : IBarcodeGenerationService
    {
        private readonly IBarcodeService _barcodeService;

        public BarcodeGenerationService(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        public async Task<BarcodeDocument> GenerateBarcodeDocument(BarcodeGenerationRequest request)
        {
            var barcodeData = new BarcodeData
            {
                Content = request.Content,
                Type = request.Type,
                Width = GetWidthForSize(request.Size),
                Height = GetHeightForSize(request.Size)
            };

            var barcodeImage = await _barcodeService.GenerateBarcode(barcodeData);

            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            if (!string.IsNullOrEmpty(request.Title))
            {
                document.Add(new Paragraph(request.Title));
            }

            var image = new Image(ImageDataFactory.Create(barcodeImage));
            document.Add(image);

            if (request.IncludeText)
            {
                document.Add(new Paragraph(request.Content));
            }

            document.Close();

            return new BarcodeDocument
            {
                Content = ms.ToArray(),
                Format = request.Format
            };
        }

        public async Task<byte[]> GenerateBarcodeLabel(BarcodeLabelRequest request)
        {
            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            var barcodeData = new BarcodeData
            {
                Content = request.Content,
                Type = request.Type,
                Width = GetWidthForSize(request.Size),
                Height = GetHeightForSize(request.Size)
            };

            for (int i = 0; i < request.Copies; i++)
            {
                if (i > 0) document.Add(new AreaBreak());

                var barcodeImage = await _barcodeService.GenerateBarcode(barcodeData);
                var image = new Image(ImageDataFactory.Create(barcodeImage));

                if (!string.IsNullOrEmpty(request.Title))
                {
                    document.Add(new Paragraph(request.Title));
                }

                document.Add(image);
            }

            document.Close();
            return ms.ToArray();
        }

        public async Task<byte[]> GenerateBulkBarcodes(BulkBarcodeRequest request)
        {
            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            foreach (var item in request.Items)
            {
                var barcodeData = new BarcodeData
                {
                    Content = item.Content,
                    Type = item.Type,
                    Width = GetWidthForSize(request.Size),
                    Height = GetHeightForSize(request.Size)
                };

                for (int i = 0; i < request.CopiesPerItem; i++)
                {
                    if (pdf.GetNumberOfPages() > 1)
                    {
                        document.Add(new AreaBreak());
                    }

                    var barcodeImage = await _barcodeService.GenerateBarcode(barcodeData);
                    var image = new Image(ImageDataFactory.Create(barcodeImage));

                    if (!string.IsNullOrEmpty(item.Title))
                    {
                        document.Add(new Paragraph(item.Title));
                    }

                    document.Add(image);

                    if (request.IncludeText)
                    {
                        document.Add(new Paragraph(item.Content));
                    }
                }
            }

            document.Close();
            return ms.ToArray();
        }

        private int GetWidthForSize(LabelSize size)
        {
            return size switch
            {
                LabelSize.Small => 144,     // 1.5" at 96 DPI
                LabelSize.Standard => 216,   // 2.25" at 96 DPI
                LabelSize.Large => 288,      // 3" at 96 DPI
                _ => 216
            };
        }

        private int GetHeightForSize(LabelSize size)
        {
            return size switch
            {
                LabelSize.Small => 48,      // 0.5" at 96 DPI
                LabelSize.Standard => 120,   // 1.25" at 96 DPI
                LabelSize.Large => 192,      // 2" at 96 DPI
                _ => 120
            };
        }
    }
}