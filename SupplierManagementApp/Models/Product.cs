using System.Text.Json.Serialization;

namespace SupplierManagementApp.Models
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set;}

        [JsonPropertyName("productName")]
        public string ProductName { get; set;}

        [JsonPropertyName("supplierId")]
        public int SupplierId { get; set;}

        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set;}

        [JsonPropertyName("quantityPerUnit")]
        public string QuantityPerUnit { get; set;}

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set;}

        [JsonPropertyName("unitsInStock")]
        public int UnitsInStock { get; set;}
    }
}