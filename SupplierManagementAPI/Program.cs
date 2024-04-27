var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints
app.MapGet("/Suppliers", () =>
{
    return DatabaseManager.GetSuppliers();
})
.WithName("GetSuppliers");

app.MapPost("/Suppliers", (Supplier supplier) => 
{
    try
    {
        DatabaseManager.CreateSupplier(supplier);
        return Results.Created($"/Products/{supplier.SupplierId}", supplier);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Failed to create supplier: {ex.Message}");
    }
})
.WithName("CreateSupplier");

app.MapGet("/Suppliers/{id:int}", (int id) => 
{
    return DatabaseManager.GetSupplierById(id);
})
.WithName("GetSupplierById");

app.MapPut("/Suppliers/{supplierId}", (int supplierId, Supplier updatedSupplier) =>
{
    var existingSupplier = DatabaseManager.GetSupplierById(supplierId);
    if (existingSupplier == null)
    {
        return Results.NotFound($"Supplier with ID {supplierId} not found.");
    }

    existingSupplier.CompanyName = updatedSupplier.CompanyName;
    existingSupplier.ContactName = updatedSupplier.ContactName;
    existingSupplier.ContactTitle = updatedSupplier.ContactTitle;
    existingSupplier.Address = updatedSupplier.Address;
    existingSupplier.City = updatedSupplier.City;
    existingSupplier.PostalCode = updatedSupplier.PostalCode;
    existingSupplier.Country = updatedSupplier.Country;
    existingSupplier.Phone = updatedSupplier.Phone;

    DatabaseManager.UpdateSupplier(existingSupplier);
    
    return Results.Ok($"Supplier with ID {supplierId} updated successfully.");
})
.WithName("UpdateSupplier");

app.MapGet("/Products", () =>
{
    return DatabaseManager.GetProducts();
})
.WithName("GetProducts");

app.MapPost("/Products", (Product product) =>
{
    try
    {
        DatabaseManager.CreateProduct(product);
        return Results.Created($"/Products/{product.ProductId}", product);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Failed to create product: {ex.Message}");
    }
})
.WithName("CreateProduct");

app.Run();
