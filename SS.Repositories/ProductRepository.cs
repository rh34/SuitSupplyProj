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
            return _productDbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateProduct(Product product)
        {
            var result = _productDbContext.Products.Update(product);
            return _productDbContext.SaveChanges() > 0;
        }

        public bool DeleteProduct(Product product)
        {
            var result = _productDbContext.Products.Remove(product);
            return result != null;
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
