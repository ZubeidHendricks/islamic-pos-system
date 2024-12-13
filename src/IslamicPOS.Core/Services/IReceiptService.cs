using IslamicPOS.Core.Models;

namespace IslamicPOS.Core.Services
{
    public interface IReceiptService
    {
        Task<string> GenerateReceipt(Transaction transaction);
        Task<byte[]> GenerateReceiptPdf(Transaction transaction);
        Task PrintReceipt(Transaction transaction, string printerName = null);
        Task<List<string>> GetAvailablePrinters();
        Task<PrinterSettings> GetPrinterSettings(string printerName);
        Task SavePrinterSettings(PrinterSettings settings);
    }

    public class PrinterSettings
    {
        public string PrinterName { get; set; }
        public bool IsDefault { get; set; }
        public string PaperSize { get; set; }
        public int Copies { get; set; } = 1;
        public bool PrintLogo { get; set; } = true;
        public bool PrintHalalCertification { get; set; } = true;
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
    }
}