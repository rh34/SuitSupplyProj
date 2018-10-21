using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SS.UI.MVC.Models;

namespace SS.UI.MVC.Clients
{
    public interface IProductApiClient
    {
        Task<ProductModel> CreateAsync(ProductModel product);
        Task<ProductModel> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<bool> PutAsync(ProductModel product, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}