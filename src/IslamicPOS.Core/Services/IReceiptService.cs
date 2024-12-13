using System.Threading.Tasks;

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
}