using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using SupplierManagementApp.Models;
using System.Text;

namespace SupplierManagementApp.Services
{
    public class SupplierManager
    {
        private readonly HttpClient _client;

        public SupplierManager(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Supplier>> GetSupplierList()
        {
            var response = await _client.GetAsync("http://localhost:5135/Suppliers");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Supplier>>(stream);
        }

        public async Task<Supplier> GetSupplierById(int id)
        {
            var response = await _client.GetAsync($"http://localhost:5135/Suppliers/{id}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Supplier>(stream);
        }

        public async Task UpdateSupplier(Supplier updatedSupplier)
        {
            var url = $"http://localhost:5135/Suppliers/{updatedSupplier.SupplierId}";
            var json = JsonSerializer.Serialize(updatedSupplier);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(url, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task SaveSupplierInfo(Supplier supplier)
        {
            try
            {
                var json = JsonSerializer.Serialize(supplier);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("http://localhost:5135/Suppliers", content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error saving supplier information: {ex.Message}");
                throw new Exception("Failed to save supplier information. Please try again.");
            }
        }


    }
}