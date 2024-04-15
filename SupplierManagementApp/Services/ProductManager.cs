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
    }
}