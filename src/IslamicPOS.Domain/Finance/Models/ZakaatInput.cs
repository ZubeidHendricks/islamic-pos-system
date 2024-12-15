using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance.Models;

public class ZakaatInput
{
    public Money Assets { get; set; } = Money.Zero();
    public Money Liabilities { get; set; } = Money.Zero();
    public Money BusinessAssets { get; set; } = Money.Zero();
    public Money Investments { get; set; } = Money.Zero();
}
