using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using SupplierManagementApp.Models;

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
            Console.WriteLine("Entered GetSupplierById - SupplierManager");
            var response = await _client.GetAsync($"http://localhost:5135/Suppliers/{id}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Supplier>(stream);
        }
    }
}