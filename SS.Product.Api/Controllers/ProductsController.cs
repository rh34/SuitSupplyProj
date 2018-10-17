using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS.Product.Api.Models;
using SS.Services;

namespace SS.Product.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("", Name = "GetProducts")]
        public IActionResult Get()
        {
            //TODO:get product
            return Ok(new List<ProductDto>(){new ProductDto(), new ProductDto()});
        }
        
        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult Get(Guid id)
        {
            //TODO:get product
            return Ok(new ProductDto());
        }

        [HttpPost]
        public IActionResult Post(ProductDto product)
        {
            //TODO:create product
            return CreatedAtRoute("GetProductById", product.Id);
        }

        [HttpPut]
        public IActionResult Put(ProductDto product)
        {
            //TODO:update product
            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            //TODO:delete product
            return NoContent();
        }
    }
}