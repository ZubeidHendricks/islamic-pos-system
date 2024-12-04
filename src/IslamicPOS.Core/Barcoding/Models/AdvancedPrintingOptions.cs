using System;

namespace IslamicPOS.Core.Barcoding.Models
{
    public class AdvancedPrintingOptions
    {
        public LogoOptions Logo { get; set; }
        public TemplateOptions Template { get; set; }
        public PrinterSettings PrinterSettings { get; set; }
    }

    public class LogoOptions
    {
        public byte[] LogoData { get; set; }
        public string LogoUrl { get; set; }
        public LogoPosition Position { get; set; } = LogoPosition.Top;
        public int MaxWidth { get; set; } = 100;
        public int MaxHeight { get; set; } = 100;
        public float Opacity { get; set; } = 1.0f;
    }

    public class TemplateOptions
    {
        public string TemplateId { get; set; }
        public bool IncludePrice { get; set; }
        public bool IncludeDescription { get; set; }
        public bool IncludeDate { get; set; }
        public string CompanyName { get; set; }
        public string CustomText { get; set; }
        public FontSettings FontSettings { get; set; }
    }

    public class PrinterSettings
    {
        public string PrinterName { get; set; }
        public string PaperSize { get; set; }
        public PrintResolution Resolution { get; set; }
        public bool DirectThermalPrinting { get; set; }
        public int DPI { get; set; } = 203; // Standard thermal printer DPI
    }

    public class FontSettings
    {
        public string TitleFont { get; set; } = "Arial";
        public string ContentFont { get; set; } = "Arial";
        public float TitleSize { get; set; } = 12;
        public float ContentSize { get; set; } = 10;
        public bool BoldTitle { get; set; } = true;
    }

    public enum LogoPosition
    {
        Top,
        Bottom,
        Left,
        Right,
        Background,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public enum PrintResolution
    {
        Low = 203,    // Standard thermal printer
        Medium = 300,  // High quality thermal printer
        High = 600    // Standard laser printer
    }
}