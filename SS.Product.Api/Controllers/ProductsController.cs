using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS.Entities.Data;
using SS.Product.Api.Models;
using SS.Services;

namespace SS.Product.Api.Controllers
{
    /// <summary>
    /// Products controller
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name = "GetProducts")]
        public IActionResult Get()
        {
            var products = _productService.GetProducts();
            if (products == null)
            { 
                return NotFound();
            }

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productDtos);
        }

        /// <summary>
        /// Get single product by Id
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="productForCreation">Product data for creation</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ProductForCreationDto productForCreation)
        {
            var product = _mapper.Map<Entities.Data.Product>(productForCreation);

            var result = _productService.CreateProduct(product);

            return CreatedAtRoute("GetProductById", new {id = product.Id}, product);
        }

        /// <summary>
        /// Update full product
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <param name="productDto">Product data to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] ProductDto productDto)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var s = _mapper.Map(productDto, product);
            s.Id = id;
            var success =_productService.UpdateProduct(s);

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
        [HttpDelete("id")]
        public IActionResult Delete([FromQuery] Guid id)
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