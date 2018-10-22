using System;
using System.Collections.Generic;
using AutoMapper;
using SS.Product.Api.Dto.Product.Input;
using SS.Product.Api.Dto.Product.Output;
using SS.Repositories;

namespace SS.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IEnumerable<ProductDto> GetProducts()
        {
            var products = _productRepository.GetProducts();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public ProductDto GetProductById(Guid id)
        {
            var product = _productRepository.GetProductById(id);
            return _mapper.Map<ProductDto>(product);
        }

        public bool UpdateProduct(Guid id, ProductForUpdateDto productDto)
        {
            var productExisting = _productRepository.GetProductById(id);

            _mapper.Map(productDto, productExisting);

            productExisting.LastUpdated = DateTime.Now;
            return _productRepository.UpdateProduct(productExisting);
        }

        public bool DeleteProduct(ProductDto productDto)
        {
            var productExisting = _productRepository.GetProductById(productDto.Id);
            return _productRepository.DeleteProduct(productExisting);
        }

        public ProductDto CreateProduct(ProductForCreationDto productDto)
        {
            var product = _mapper.Map<Entities.Data.Product>(productDto);

            product.Created = DateTime.Now;
            product.LastUpdated = DateTime.Now;
            _productRepository.CreateProduct(product);

            return _mapper.Map<ProductDto>(product);
        }
    }
}