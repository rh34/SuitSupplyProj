using System;
using System.Collections.Generic;
using SS.Entities.Data;

namespace SS.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(Guid id);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool CreateProduct(Product product);
    }
}
