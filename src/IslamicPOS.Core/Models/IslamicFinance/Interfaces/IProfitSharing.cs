namespace IslamicPOS.Core.Models.IslamicFinance.Interfaces;

public interface IProfitSharing
{
    decimal TotalAmount { get; set; }
    DateTime PeriodStart { get; set; }
    DateTime PeriodEnd { get; set; }
    string Period { get; set; }
    bool IsDistributed { get; set; }
    DateTime? DistributedAt { get; set; }
}