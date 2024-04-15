using System.Text.Json.Serialization;

namespace SupplierManagementApp.Models
{
    public class Supplier
    {
        [JsonPropertyName("supplierId")]
        public int SupplierId { get; set;}

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set;}

        [JsonPropertyName("contactName")]
        public string ContactName { get; set;}

        [JsonPropertyName("contactTitle")]
        public string ContactTitle { get; set;}

        [JsonPropertyName("address")]
        public string Address { get; set;}

        [JsonPropertyName("city")]
        public string City { get; set;}

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set;}

        [JsonPropertyName("country")]
        public string Country { get; set;}

        [JsonPropertyName("phone")]
        public string Phone { get; set;}
    }
}