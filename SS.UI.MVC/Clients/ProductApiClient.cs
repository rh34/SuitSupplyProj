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

        private const string _version = "1.0";

        public ProductApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductApi");
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/{_version}/products/", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductModel>();
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/{_version}/products/{id}");

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
            var response = await _httpClient.GetAsync($"/api/{_version}/products");

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
            var response = await _httpClient.PutAsJsonAsync($"/api/{_version}/products/{id}", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/{_version}/products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<bool>();
        }
    }
}