using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS.UI.MVC.Models;

namespace SS.UI.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<ProductModel> products = _productApiClient.GetProductsAsync().Result;
            //return View(products);

            return View("List", products);
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel product = _productApiClient.GetProductByIdAsync(id).Result;
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] ProductModel productModel, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _productApiClient.CreateAsync(productModel).Result;
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(Guid id)
        {
            ProductModel product = _productApiClient.GetProductByIdAsync(id).Result;
            return View(product); 
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind] ProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _productApiClient.PutAsync(productModel, id).Result;
                    if (result)
                        return RedirectToAction("Index");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(Guid id)
        {
            ProductModel product = _productApiClient.GetProductByIdAsync(id).Result;
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var result = _productApiClient.DeleteAsync(id).Result;
                 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

    public interface IProductApiClient
    {
        Task<ProductModel> CreateAsync(ProductModel product);
        Task<ProductModel> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<bool> PutAsync(ProductModel product, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }

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