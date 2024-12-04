using IslamicPOS.Core.Models.Wholesale;
using Microsoft.AspNetCore.Components;

namespace IslamicPOS.Web.Pages.Wholesale;

public partial class VendorPortal
{
    private bool _loading = true;
    private string _searchString = "";
    private VendorScore _vendorScore = new();
    private List<VendorProduct> _products = new();
    private List<VendorOrder> _orders = new();
    private List<QualityControl> _inspections = new();
    private Dictionary<Guid, List<QualityControl>> _productInspections = new();
    private VendorProduct _selectedProduct;

    protected override async Task OnInitializedAsync()
    {
        await LoadVendorData();
    }

    private async Task LoadVendorData()
    {
        try
        {
            _loading = true;

            var vendorId = GetCurrentVendorId(); // TODO: Get from authentication
            var tasks = new[]
            {
                LoadVendorScore(vendorId),
                LoadProducts(vendorId),
                LoadOrders(vendorId),
                LoadInspections(vendorId)
            };

            await Task.WhenAll(tasks);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task LoadVendorScore(int vendorId)
    {
        _vendorScore = await VendorService.GetVendorScore(vendorId);
    }

    private async Task LoadProducts(int vendorId)
    {
        _products = await VendorService.GetVendorProducts(vendorId);
    }

    private async Task LoadOrders(int vendorId)
    {
        _orders = await VendorService.GetVendorOrders(vendorId);
    }

    private async Task LoadInspections(int vendorId)
    {
        _inspections = await QualityService.GetInspectionsByVendor(vendorId);
        _productInspections = _inspections
            .GroupBy(i => i.ProductId)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    private async Task AddProduct()
    {
        var parameters = new DialogParameters
        {
            { "VendorId", GetCurrentVendorId() },
            { "VendorScore", _vendorScore }
        };

        var dialog = await DialogService.ShowAsync<AddProductDialog>("Add Product", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadVendorData();
            Snackbar.Add("Product added successfully", Severity.Success);
        }
    }

    private async Task EditProduct(VendorProduct product)
    {
        var parameters = new DialogParameters
        {
            { "Product", product },
            { "VendorScore", _vendorScore }
        };

        var dialog = await DialogService.ShowAsync<EditProductDialog>("Edit Product", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadVendorData();
            Snackbar.Add("Product updated successfully", Severity.Success);
        }
    }

    private async Task ViewInspections(VendorProduct product)
    {
        var parameters = new DialogParameters
        {
            { "ProductId", product.Id },
            { "ProductName", product.Name },
            { "Inspections", _productInspections.GetValueOrDefault(product.Id, new()) }
        };

        var dialog = await DialogService.ShowAsync<InspectionHistoryDialog>("Inspection History", parameters);
        await dialog.Result;
    }

    private async Task UpdateStock(VendorProduct product)
    {
        var parameters = new DialogParameters
        {
            { "Product", product }
        };

        var dialog = await DialogService.ShowAsync<UpdateStockDialog>("Update Stock", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadVendorData();
            Snackbar.Add("Stock updated successfully", Severity.Success);
        }
    }

    private async Task UpdateHalalStatus(VendorProduct product)
    {
        var parameters = new DialogParameters
        {
            { "Product", product }
        };

        var dialog = await DialogService.ShowAsync<HalalCertificationDialog>("Update Halal Status", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadVendorData();
            Snackbar.Add("Halal status updated successfully", Severity.Success);
        }
    }

    private async Task ViewOrder(VendorOrder order)
    {
        var parameters = new DialogParameters
        {
            { "Order", order }
        };

        var dialog = await DialogService.ShowAsync<OrderDetailsDialog>("Order Details", parameters);
        await dialog.Result;
    }

    private bool FilterProducts(VendorProduct product) =>
        string.IsNullOrWhiteSpace(_searchString) ||
        product.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase) ||
        product.SKU.Contains(_searchString, StringComparison.OrdinalIgnoreCase);

    private bool FilterOrders(VendorOrder order) =>
        string.IsNullOrWhiteSpace(_searchString) ||
        order.OrderNumber.Contains(_searchString, StringComparison.OrdinalIgnoreCase);

    private string GetProductName(Guid productId) =>
        _products.FirstOrDefault(p => p.Id == productId)?.Name ?? "Unknown Product";

    private Color GetScoreColor(decimal score) => score switch
    {
        >= 90 => Color.Success,
        >= 70 => Color.Warning,
        _ => Color.Error
    };

    private Color GetStatusColor(decimal score) => score switch
    {
        >= 85 => Color.Success,
        >= 70 => Color.Warning,
        _ => Color.Error
    };

    private Color GetInspectionColor(InspectionResult result) => result switch
    {
        InspectionResult.Pass => Color.Success,
        InspectionResult.PassWithObservations => Color.Info,
        InspectionResult.MinorDefects => Color.Warning,
        _ => Color.Error
    };

    private Color GetOrderStatusColor(string status) => status switch
    {
        "Completed" => Color.Success,
        "Pending" => Color.Warning,
        "Processing" => Color.Info,
        "Cancelled" => Color.Error,
        _ => Color.Default
    };

    private int GetCurrentVendorId()
    {
        // TODO: Get from authentication context
        return 1; // Temporary
    }
}