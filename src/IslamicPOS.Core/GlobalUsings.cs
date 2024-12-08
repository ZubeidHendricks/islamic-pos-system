// Framework & Standard Libraries
global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Linq;

// Microsoft Extensions
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

// Entity Framework Core
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// Domain Models
global using IslamicPOS.Core.Models.Common;
global using IslamicPOS.Core.Models.Products;
global using IslamicPOS.Core.Models.Transactions;
global using IslamicPOS.Core.Models.Auth;
global using IslamicPOS.Core.Models.Logistics;
global using IslamicPOS.Core.Models.Reports;
global using IslamicPOS.Core.Models.IslamicFinance;

// Interfaces & Services
global using IslamicPOS.Core.Interfaces;
global using IslamicPOS.Core.Services;
global using IslamicPOS.Core.Services.Auth;
global using IslamicPOS.Core.Services.Delivery;
global using IslamicPOS.Core.Services.Financial;
global using IslamicPOS.Core.Services.Reports;