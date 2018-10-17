using System;
using SS.Entities.Data;

namespace SS.Services
{
    public interface IProductService
    {
        Product GetProductById(Guid id);
        Product UpdateProduct(Guid id, Product product);
        bool DeleteProduct(Guid id);
        bool CreateProduct(Product product);
    }
}
