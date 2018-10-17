using System;
using System.Collections.Generic;
using SS.Entities.Data;

namespace SS.Repositories
{
    public interface IProductRepository
    {
        Product GetProductById(Guid id);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool CreateProduct(Product product);
        IEnumerable<Product> GetProducts();
    }
}