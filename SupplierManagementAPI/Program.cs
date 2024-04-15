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

app.MapGet("/Products", () =>
{
    return DatabaseManager.GetProducts();
})
.WithName("GetProducts");

app.Run();
