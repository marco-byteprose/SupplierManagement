using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using SupplierManagementApp.Models;

namespace SupplierManagementApp.Services
{
    public class ProductManager
    {
        private readonly HttpClient _client;

        public ProductManager(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Product>> GetProductList()
        {
            var response = await _client.GetAsync("http://localhost:5135/Products");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(stream);
        }

        public async Task SaveProductInfo(Product product)
        {
            try
            {
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("http://localhost:5135/Products", content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error saving product information: {ex.Message}");
                throw new Exception("Failed to save product information. Please try again.");
            }
        }
    }
}