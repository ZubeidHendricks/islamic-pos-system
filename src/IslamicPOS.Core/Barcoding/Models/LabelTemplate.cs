using System;

namespace IslamicPOS.Core.Barcoding.Models
{
    public class LabelTemplate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LabelSize Size { get; set; }
        public TemplateLayout Layout { get; set; }
        public TemplateStyling Styling { get; set; }
        public bool IsDefault { get; set; }
    }

    public class TemplateLayout
    {
        public float LogoWidth { get; set; }
        public float LogoHeight { get; set; }
        public float BarcodeWidth { get; set; }
        public float BarcodeHeight { get; set; }
        public float TitleHeight { get; set; }
        public float ContentHeight { get; set; }
        public float Margin { get; set; }
        public float Padding { get; set; }
        public bool RotateBarcode { get; set; }
    }

    public class TemplateStyling
    {
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public string TextColor { get; set; } = "#000000";
        public string BorderColor { get; set; }
        public float BorderWidth { get; set; }
        public string FontFamily { get; set; }
        public bool RoundedCorners { get; set; }
        public float CornerRadius { get; set; }
    }
}