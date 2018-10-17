using System;
using System.Collections.Generic;
using System.Linq;
using SS.Entities.Data;

namespace SS.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public Product GetProductById(Guid id)
        {
            return _productDbContext.Products.First();
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Guid id, Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool CreateProduct(Product product)
        {
            _productDbContext.Products.Add(product);
            return _productDbContext.SaveChanges() > 0;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productDbContext.Products;
        }
    }
}
