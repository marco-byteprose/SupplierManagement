using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupplierManagementApp.Services;
using SupplierManagementApp.Models;

public class CreateProductModel : PageModel
{
    private readonly ProductManager _productManager;

    [BindProperty]
    public Product Product { get; set; }

    [BindProperty(SupportsGet = true)]
    public int SupplierId { get; set; }

    public CreateProductModel(ProductManager productManager)
    {
        _productManager = productManager;
    }

    public void OnGet()
    {
        // Accessing the SupplierId from the query string
        if (Request.Query.TryGetValue("supplierId", out var supplierId))
        {
            SupplierId = int.Parse(supplierId);
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        int supplierId = 0;
        if (Request.Query.TryGetValue("supplierId", out var supplierIdQuery))
        {
            int.TryParse(supplierIdQuery, out supplierId);
        }

        Product.SupplierId = supplierId;
        
        try
        {
            await _productManager.SaveProductInfo(Product);
            return RedirectToPage("/SupplierDetails", new { id = Product.SupplierId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Failed to save product information. Please verify information and try again.";
            return Page();
        }
    }

}