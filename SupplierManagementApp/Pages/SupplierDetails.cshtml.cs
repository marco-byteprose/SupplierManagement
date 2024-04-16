using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupplierManagementApp.Models;
using SupplierManagementApp.Services;

public class SupplierDetailsModel : PageModel
{
    private readonly SupplierManager _supplierManager;

    public SupplierDetailsModel(SupplierManager supplierManager)
    {
        _supplierManager = supplierManager;
    }

    public Supplier Supplier { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        try
        {
            Supplier = await _supplierManager.GetSupplierById(id);
            return Page();
        }
        catch (Exception ex)
        {
            return RedirectToPage("/Error");
        }
    }
}