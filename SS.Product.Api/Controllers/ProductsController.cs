﻿using System;
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

        [HttpGet("", Name = "GetProducts")]
        public IActionResult Get()
        {
            var products = _productService.GetProducts();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productDtos);
        }
        
        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult Get(Guid id)
        {
            var product = _productService.GetProductById(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        [HttpPost]
        public IActionResult Post(ProductForCreationDto productForCreation)
        {
            var product = _mapper.Map<Entities.Data.Product>(productForCreation);

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