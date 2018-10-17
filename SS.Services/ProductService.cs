using System;
using System.Collections.Generic;
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
        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public Product GetProductById(Guid id)
        {
            return _productRepository.GetProductById(id);
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
            return _productRepository.CreateProduct(product);
        }
    }
}