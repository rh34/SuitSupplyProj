using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS.Entities.Data;
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
            var products = _productService.GetProducts();
            return Ok(products);
        }
        
        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult Get(Guid id)
        {
            var product = _productService.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(ProductForCreationDto productForCreation)
        {
            var newId = Guid.NewGuid();
            var product = new Entities.Data.Product
            {
                Id = newId,
                Name = productForCreation.Name,
                Price = productForCreation.Price,
                PhotoUrl = productForCreation.PhotoUrl,
                LastUpdated = productForCreation.LastUpdated,
                Currency = (CurrencyEnum)productForCreation.Currency
            };

            var result = _productService.CreateProduct(product);
            return CreatedAtRoute("GetProductById", new {id = product.Id}, product);
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