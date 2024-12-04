using System.Net.Mail;

namespace IslamicPOS.Infrastructure.Services;

public class EmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromAddress;

    public EmailService(string smtpServer, int port, string username, string password, string fromAddress)
    {
        _smtpClient = new SmtpClient(smtpServer, port)
        {
            Credentials = new System.Net.NetworkCredential(username, password),
            EnableSsl = true
        };
        _fromAddress = fromAddress;
    }

    public async Task SendLowStockAlert(string productName, int currentStock)
    {
        var subject = $"Low Stock Alert: {productName}";
        var body = $"Product {productName} is running low. Current stock: {currentStock}";
        
        var message = new MailMessage(_fromAddress, "manager@example.com", subject, body);
        await _smtpClient.SendMailAsync(message);
    }

    public async Task SendTransactionReceipt(string customerEmail, string receiptHtml)
    {
        var message = new MailMessage(_fromAddress, customerEmail, "Transaction Receipt", receiptHtml)
        {
            IsBodyHtml = true
        };
        await _smtpClient.SendMailAsync(message);
    }
}