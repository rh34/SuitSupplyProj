using System;
using SS.Entities.Data;

namespace SS.Repositories
{
    public interface IProductRepository
    {
        Product GetProductById(Guid id);
        Product UpdateProduct(Guid id, Product product);
        bool DeleteProduct(Guid id);
        bool CreateProduct(Product product);
    }
}