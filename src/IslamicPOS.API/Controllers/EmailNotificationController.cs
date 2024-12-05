using System;
using System.Threading.Tasks;
using IslamicPOS.Core.Email.Interfaces;
using IslamicPOS.Core.Email.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmailNotificationController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ITemplateRenderer _templateRenderer;
        private readonly ILogger<EmailNotificationController> _logger;

        public EmailNotificationController(
            IEmailService emailService,
            ITemplateRenderer templateRenderer,
            ILogger<EmailNotificationController> logger)
        {
            _emailService = emailService;
            _templateRenderer = templateRenderer;
            _logger = logger;
        }

        [HttpPost("send")]
        [Authorize(Roles = "SystemAdmin,TenantAdmin")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            try
            {
                var message = new EmailMessage
                {
                    To = request.To,
                    Subject = request.Subject,
                    HtmlContent = request.HtmlContent,
                    TextContent = request.TextContent
                };

                await _emailService.SendEmailAsync(message);
                return Ok(new { Message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Recipient}", request.To);
                return StatusCode(500, new { Message = "Failed to send email" });
            }
        }

        [HttpPost("send-template")]
        [Authorize(Roles = "SystemAdmin,TenantAdmin")]
        public async Task<IActionResult> SendTemplatedEmail([FromBody] SendTemplatedEmailRequest request)
        {
            try
            {
                await _emailService.SendTemplatedEmailAsync(
                    request.TemplateName,
                    request.To,
                    request.Variables);

                return Ok(new { Message = "Templated email sent successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send templated email to {Recipient}", request.To);
                return StatusCode(500, new { Message = "Failed to send templated email" });
            }
        }

        [HttpGet("templates")]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> GetTemplates()
        {
            try
            {
                var templates = await _templateRenderer.GetAllTemplatesAsync();
                return Ok(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve email templates");
                return StatusCode(500, new { Message = "Failed to retrieve templates" });
            }
        }

        public class SendEmailRequest
        {
            public string To { get; set; }
            public string Subject { get; set; }
            public string HtmlContent { get; set; }
            public string TextContent { get; set; }
        }

        public class SendTemplatedEmailRequest
        {
            public string TemplateName { get; set; }
            public string To { get; set; }
            public Dictionary<string, string> Variables { get; set; }
        }
    }
}