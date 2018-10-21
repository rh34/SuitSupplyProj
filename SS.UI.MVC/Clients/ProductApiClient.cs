using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SS.UI.MVC.Models;

namespace SS.UI.MVC.Clients
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpClient;
        public ProductApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductApi");
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/products/", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductModel>();
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");

            try
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ProductModel>();
            }
            catch (Exception e)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"/api/products");

            try
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<ProductModel>>();
            }
            catch (Exception e)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        public async Task<bool> PutAsync(ProductModel product, Guid id)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/products/{id}", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>();
        }
    }
}