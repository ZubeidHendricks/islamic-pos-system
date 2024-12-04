# Islamic POS System

A modern Point of Sale system built with Blazor WebAssembly, featuring Islamic finance capabilities including Zakaah calculation and profit sharing.

## Features

- Modern, responsive UI built with MudBlazor
- Point of Sale interface with barcode support
- Inventory management
- Zakaah calculation
- Profit sharing and distribution
- Offline capabilities
- Multi-language support (English/Arabic)

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
```bash
git clone https://github.com/ZubeidHendricks/islamic-pos-system.git
```

2. Navigate to the project directory
```bash
cd islamic-pos-system
```

3. Run the application
```bash
dotnet run --project src/IslamicPOS.Web
```

## Project Structure

- `IslamicPOS.Web` - Blazor WebAssembly frontend
- `IslamicPOS.Core` - Core business logic and models
- `IslamicPOS.Infrastructure` - Data access and external services