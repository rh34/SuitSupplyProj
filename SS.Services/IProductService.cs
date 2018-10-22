using System;
using System.Collections.Generic;
using SS.Product.Api.Dto.Product.Input;
using SS.Product.Api.Dto.Product.Output;

namespace SS.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto GetProductById(Guid id);
        bool UpdateProduct(Guid id, ProductForUpdateDto product);
        bool DeleteProduct(ProductDto product);
        ProductDto CreateProduct(ProductForCreationDto product);
    }
}
