namespace IslamicPOS.Core.Models
{
    public class PrinterSettings
    {
        public string PrinterName { get; set; }
        public bool IsDefault { get; set; }
        public string PaperSize { get; set; } = "80mm";
        public int Copies { get; set; } = 1;
        public bool PrintLogo { get; set; } = true;
        public bool PrintHalalCertification { get; set; } = true;
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
    }
}