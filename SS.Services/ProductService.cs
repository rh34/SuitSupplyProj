using System;
using SS.Entities.Data;
using SS.Repositories;

namespace SS.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Product GetProductById(Guid id)
        {
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
            throw new NotImplementedException();
        }
    }
}