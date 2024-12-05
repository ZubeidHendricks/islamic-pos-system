using System;
using System.Threading.Tasks;
using IslamicPOS.Core.Email.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace IslamicPOS.Infrastructure.Email.Services
{
    public class EmailQueueService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailQueueService> _logger;

        public EmailQueueService(
            IServiceProvider serviceProvider,
            ILogger<EmailQueueService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        // Get pending emails
                        var pendingEmails = await dbContext.EmailQueue
                            .Where(e => e.Status == EmailStatus.Pending)
                            .OrderBy(e => e.CreatedAt)
                            .Take(10)
                            .ToListAsync();

                        foreach (var email in pendingEmails)
                        {
                            try
                            {
                                await emailService.SendEmailAsync(new EmailMessage
                                {
                                    To = email.To,
                                    Subject = email.Subject,
                                    HtmlContent = email.HtmlContent,
                                    TextContent = email.TextContent
                                });

                                email.Status = EmailStatus.Sent;
                                email.SentAt = DateTime.UtcNow;
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Failed to send email {EmailId}", email.Id);
                                email.Status = EmailStatus.Failed;
                                email.ErrorMessage = ex.Message;
                                email.RetryCount++;

                                if (email.RetryCount < 3)
                                {
                                    email.Status = EmailStatus.Pending;
                                    email.NextRetryAt = DateTime.UtcNow.AddMinutes(5 * email.RetryCount);
                                }
                            }
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in email queue processing");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}