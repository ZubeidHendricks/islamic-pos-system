namespace IslamicPOS.Core.Models.IslamicFinance;

public class ProfitSharing : Entity
{
    public Money TotalAmount { get; private set; }
    public decimal MerchantShare { get; private set; } // As a percentage
    public Money MerchantAmount { get; private set; }
    public Money PartnerAmount { get; private set; }

    public ProfitSharing(Money totalAmount, decimal merchantShare)
    {
        if (merchantShare is < 0 or > 1)
            throw new ArgumentException("Merchant share must be between 0 and 1", nameof(merchantShare));

        TotalAmount = totalAmount;
        MerchantShare = merchantShare;

        CalculateShares();
    }

    private void CalculateShares()
    {
        MerchantAmount = Money.Create(
            TotalAmount.Amount * MerchantShare, 
            TotalAmount.Currency);

        PartnerAmount = Money.Create(
            TotalAmount.Amount * (1 - MerchantShare),
            TotalAmount.Currency);
    }
}