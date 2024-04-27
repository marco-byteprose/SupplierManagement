using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class DatabaseManager
{
    public static List<Supplier> GetSuppliers()
    {
        var suppliers = new List<Supplier>();
        string connectionString = "Data Source=Northwind.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = 
                @"SELECT SupplierId, CompanyName, ContactName, ContactTitle, Address, City, PostalCode, Country, Phone FROM Suppliers;";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var supplier = new Supplier
                    {
                        SupplierId = Convert.ToInt32(reader["SupplierId"]),
                        CompanyName = reader["CompanyName"].ToString(),
                        ContactName = reader["ContactName"].ToString(),
                        ContactTitle = reader["ContactTitle"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString(),
                        PostalCode = reader["PostalCode"].ToString(),
                        Country = reader["Country"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                    suppliers.Add(supplier);
                }
            }
        }
        return suppliers;
    }

    public static Supplier GetSupplierById(int supplierId)
    {
        Supplier supplier = null;
        string connectionString = "Data Source=Northwind.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = 
                @"SELECT SupplierId, CompanyName, ContactName, ContactTitle, Address, City, PostalCode, Country, Phone
                  FROM Suppliers
                  WHERE SupplierId = @SupplierId;";
            command.Parameters.AddWithValue("@SupplierId", supplierId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    supplier = new Supplier
                    {
                        SupplierId = Convert.ToInt32(reader["SupplierId"]),
                        CompanyName = reader["CompanyName"].ToString(),
                        ContactName = reader["ContactName"].ToString(),
                        ContactTitle = reader["ContactTitle"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString(),
                        PostalCode = reader["PostalCode"].ToString(),
                        Country = reader["Country"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Products = new List<Product>()
                    };
                }
            }

            // Retrieve products if supplierId found
            if (supplier != null)
            {
                command.Parameters.Clear();
                command.CommandText = 
                    @"SELECT ProductId, ProductName, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock
                      FROM Products
                      WHERE SupplierId = @SupplierId;";
                command.Parameters.AddWithValue("@SupplierId", supplierId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            QuantityPerUnit = reader["QuantityPerUnit"].ToString(),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                            UnitsInStock = Convert.ToInt32(reader["UnitsInStock"])
                        };
                        supplier.Products.Add(product);
                    }
                }
            }
        }
        return supplier;
    }

    public static bool UpdateSupplier(Supplier supplier)
    {
        int supplierId = supplier.SupplierId;
        string supplierName = supplier.CompanyName;
        string supplierContact = supplier.ContactName;
        string supplierContactTitle = supplier.ContactTitle;
        string supplierPhone = supplier.Phone;
        string supplierAddress = supplier.Address;
        string supplierCity = supplier.City;
        string supplierPostalCode = supplier.PostalCode;
        string supplierCountry = supplier.Country;

        string connectionString = "Data Source=Northwind.db";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();

                    command.CommandText = @"
                        UPDATE Suppliers
                        SET CompanyName = @supplierName, ContactName = @supplierContact, ContactTitle = @supplierContactTitle, Phone = @supplierPhone, Address = @supplierAddress, City = @supplierCity, PostalCode = @supplierPostalCode, Country = @supplierCountry
                        WHERE SupplierId = @supplierId;
                    ";

                    // Parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@supplierId", supplierId);
                    command.Parameters.AddWithValue("@supplierName", supplierName);
                    command.Parameters.AddWithValue("@supplierContact", supplierContact);
                    command.Parameters.AddWithValue("@supplierContactTitle", supplierContactTitle);
                    command.Parameters.AddWithValue("@supplierPhone", supplierPhone);
                    command.Parameters.AddWithValue("@supplierAddress", supplierAddress);
                    command.Parameters.AddWithValue("@supplierCity", supplierCity);
                    command.Parameters.AddWithValue("@supplierPostalCode", supplierPostalCode);
                    command.Parameters.AddWithValue("@supplierCountry", supplierCountry);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
            Console.WriteLine("Supplier updated successfully.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public static void CreateSupplier(Supplier supplier)
    {
        string connectionString = "Data Source=Northwind.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Suppliers (SupplierId, CompanyName, ContactName, ContactTitle, Address, City, PostalCode, Country, Phone) VALUES (@SupplierId, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @PostalCode, @Country, @Phone);";
                
                command.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
                command.Parameters.AddWithValue("@CompanyName", supplier.CompanyName);
                command.Parameters.AddWithValue("@ContactName", supplier.ContactName);
                command.Parameters.AddWithValue("@ContactTitle", supplier.ContactTitle);
                command.Parameters.AddWithValue("@Address", supplier.Address);
                command.Parameters.AddWithValue("@City", supplier.City);
                command.Parameters.AddWithValue("@PostalCode", supplier.PostalCode);
                command.Parameters.AddWithValue("@Country", supplier.Country);
                command.Parameters.AddWithValue("@Phone", supplier.Phone);
                
                command.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                throw new Exception($"Error creating supplier: {ex.Message}");
            }
        }
    }

    public static List<Product> GetProducts()
    {
        var products = new List<Product>();
        string connectionString = "Data Source=Northwind.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = 
                @"SELECT ProductId, ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock FROM Products;";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        SupplierId = Convert.ToInt32(reader["SupplierId"]),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        QuantityPerUnit = reader["QuantityPerUnit"].ToString(),
                        UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                        UnitsInStock = Convert.ToInt32(reader["UnitsInStock"])
                    };
                    products.Add(product);
                }
            }
        }
        return products;
    }

    public static void CreateProduct(Product product)
    {
        string connectionString = "Data Source=Northwind.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Products (ProductId, ProductName, SupplierId, CategoryId, QuantityPerUnit, UnitPrice, UnitsInStock) VALUES (@ProductId, @ProductName, @SupplierId, @CategoryId, @QuantityPerUnit, @UnitPrice, @UnitsInStock);";
                
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@SupplierId", product.SupplierId);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
                
                command.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                throw new Exception($"Error creating product: {ex.Message}");
            }
        }
    }
}