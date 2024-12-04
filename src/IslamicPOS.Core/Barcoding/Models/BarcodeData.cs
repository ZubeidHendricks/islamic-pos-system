using System;

namespace IslamicPOS.Core.Barcoding.Models
{
    public class BarcodeData
    {
        public string Content { get; set; }
        public BarcodeType Type { get; set; }
        public int Width { get; set; } = 300;
        public int Height { get; set; } = 100;
        public string Format { get; set; } = "PNG";
    }

    public enum BarcodeType
    {
        QRCode,
        Code128,
        EAN13,
        UPC
    }
}