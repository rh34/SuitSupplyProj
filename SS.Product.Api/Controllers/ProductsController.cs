using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SS.Product.Api.Dto.Product.Input;
using SS.Product.Api.Dto.Product.Output;
using SS.Services;

namespace SS.Product.Api.Controllers
{
    /// <summary>
    /// Products controller
    /// </summary>
    [Route("api/{v:apiVersion}/products")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
         

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name = "GetProducts")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var products = _productService.GetProducts();
            if (products == null)
            { 
                return NotFound();
            }

            return Ok(products);
        }

        /// <summary>
        /// Get single product by Id
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(404)]
        public IActionResult Get([FromRoute] Guid id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="productForCreation">Product data for creation</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ProductForCreationDto productForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var product = _mapper.Map<Entities.Data.Product>(productForCreation);

            var result = _productService.CreateProduct(productForCreation);

            return CreatedAtRoute("GetProductById", new {id = result.Id}, result);
        }

        /// <summary>
        /// Update full product
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <param name="productDto">Product data to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Put([FromRoute] Guid id, [FromBody] ProductForUpdateDto productDto)
        {
            var productFromRepo = _productService.GetProductById(id);

            if (productFromRepo == null)
            {
                return NotFound();
            }

            //var productUpdated = _mapper.Map(productDto, productFromRepo);
            //productUpdated.Id = id;
            
            var success =_productService.UpdateProduct(id, productDto);

            if (success)
            {
                return NoContent();
            }

            throw new Exception($"An error occured while updating the product with Id: {id}");
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var result = _productService.DeleteProduct(product);

            return NoContent();
        }
    }
}