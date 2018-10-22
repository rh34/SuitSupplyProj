using System;
using System.Collections.Generic;
using SS.Entities.Data;

namespace SS.Services
{
    public interface IProductService
    {
        IEnumerable<Entities.Data.Product> GetProducts();
        Entities.Data.Product GetProductById(Guid id);
        bool UpdateProduct(Entities.Data.Product product);
        bool DeleteProduct(Entities.Data.Product product);
        bool CreateProduct(Entities.Data.Product product);
    }
}
