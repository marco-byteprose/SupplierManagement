using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using SupplierManagementApp.Models;
using SupplierManagementApp.Services;

public class ListSupplierModel : PageModel
{
    private readonly SupplierManager _supplierManager;

    public ListSupplierModel(SupplierManager supplierManager)
    {
        _supplierManager = supplierManager;
    }

    public IEnumerable<Supplier> Suppliers { get; private set;}

    public async Task OnGetAsync()
    {
        Suppliers = await _supplierManager.GetSupplierList();
    }
}