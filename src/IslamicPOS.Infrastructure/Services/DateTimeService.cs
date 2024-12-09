using IslamicPOS.Application.Common.Interfaces;

namespace IslamicPOS.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}