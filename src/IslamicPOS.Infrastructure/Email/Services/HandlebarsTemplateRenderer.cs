using System;
using System.Threading.Tasks;
using HandlebarsDotNet;
using IslamicPOS.Core.Email.Models;
using IslamicPOS.Core.Email.Interfaces;

namespace IslamicPOS.Infrastructure.Email.Services
{
    public class HandlebarsTemplateRenderer : ITemplateRenderer
    {
        private readonly ILogger<HandlebarsTemplateRenderer> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHandlebars _handlebars;

        public HandlebarsTemplateRenderer(
            ILogger<HandlebarsTemplateRenderer> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _handlebars = Handlebars.Create();
            RegisterHelpers();
        }

        private void RegisterHelpers()
        {
            // Date formatting helper
            _handlebars.RegisterHelper("formatDate", (context, args) =>
            {
                if (args.Length < 1) return "";
                if (DateTime.TryParse(args[0]?.ToString(), out var date))
                {
                    var format = args.Length > 1 ? args[1]?.ToString() : "d";
                    return date.ToString(format);
                }
                return args[0]?.ToString() ?? "";
            });

            // Currency formatting helper
            _handlebars.RegisterHelper("formatCurrency", (context, args) =>
            {
                if (args.Length < 1) return "";
                if (decimal.TryParse(args[0]?.ToString(), out var amount))
                {
                    var currency = args.Length > 1 ? args[1]?.ToString() : "USD";
                    return amount.ToString("C", new System.Globalization.CultureInfo("en-US"));
                }
                return args[0]?.ToString() ?? "";
            });

            // Conditional helper
            _handlebars.RegisterHelper("ifEquals", (context, args) =>
            {
                if (args.Length < 2) return "";
                return args[0]?.ToString() == args[1]?.ToString();
            });

            // Array helper
            _handlebars.RegisterHelper("join", (context, args) =>
            {
                if (args.Length < 1) return "";
                if (args[0] is System.Collections.IEnumerable enumerable)
                {
                    var separator = args.Length > 1 ? args[1]?.ToString() ?? ", " : ", ";
                    return string.Join(separator, enumerable.Cast<object>());
                }
                return args[0]?.ToString() ?? "";
            });

            // URL helper
            _handlebars.RegisterHelper("url", (context, args) =>
            {
                if (args.Length < 1) return "";
                var path = args[0]?.ToString() ?? "";
                if (!path.StartsWith("/")) path = "/" + path;
                return $"https://app.islamicpos.com{path}";
            });
        }

        public async Task<EmailTemplate> GetTemplateAsync(string templateName)
        {
            try
            {
                var template = await _context.EmailTemplates
                    .FirstOrDefaultAsync(t => t.Name == templateName && t.IsActive);

                if (template == null)
                {
                    _logger.LogWarning("Template {TemplateName} not found", templateName);
                    return null;
                }

                return template;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving template {TemplateName}", templateName);
                throw;
            }
        }

        public async Task<string> RenderTemplateAsync(string template, Dictionary<string, string> variables)
        {
            try
            {
                var compiledTemplate = _handlebars.Compile(template);
                return await Task.FromResult(compiledTemplate(variables));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rendering template");
                throw;
            }
        }
    }
}