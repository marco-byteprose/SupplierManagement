using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupplierManagementApp.Services;
using SupplierManagementApp.Models;

public class CreateSupplierModel : PageModel
{
    private readonly SupplierManager _supplierManager;

    [BindProperty]
    public Supplier Supplier { get; set; }

    public CreateSupplierModel(SupplierManager supplierManager)
    {
        _supplierManager = supplierManager;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _supplierManager.SaveSupplierInfo(Supplier);
            return RedirectToPage("./ListSupplier");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Failed to save supplier information. Please verify information and try again.";
            return Page();
        }
    }
}