using System.Threading.Tasks;
using IslamicPOS.Core.Barcoding.Models;

namespace IslamicPOS.Core.Barcoding.Interfaces
{
    public interface IBarcodeService
    {
        Task<byte[]> GenerateBarcode(BarcodeData data);
        Task<byte[]> GenerateQRCode(string content, int size = 300);
        Task<string> DecodeBarcode(byte[] barcodeImage);
    }
}