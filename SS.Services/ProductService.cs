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
        public IEnumerable<Entities.Data.Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public Entities.Data.Product GetProductById(Guid id)
        {
            return _productRepository.GetProductById(id);
        }

        public bool UpdateProduct(Entities.Data.Product product)
        {
            product.LastUpdated = DateTime.Now;
            return _productRepository.UpdateProduct(product);
        }

        public bool DeleteProduct(Entities.Data.Product product)
        {
            return _productRepository.DeleteProduct(product);
        }

        public bool CreateProduct(Entities.Data.Product product)
        {
            product.Created = DateTime.Now;
            product.LastUpdated = DateTime.Now;
            return _productRepository.CreateProduct(product);
        }
    }
}