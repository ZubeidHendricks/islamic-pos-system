namespace IslamicPOS.Domain.Sales;

public class ProductItem
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public bool IsHalal { get; private set; }
    public string HalalCertification { get; private set; }

    public ProductItem(string id, string name, bool isHalal = true, string halalCertification = "")
    {
        Id = id;
        Name = name;
        IsHalal = isHalal;
        HalalCertification = halalCertification;
    }
}
