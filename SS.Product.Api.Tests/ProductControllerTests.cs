using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SS.Product.Api.Controllers;
using SS.Product.Api.Models;
using SS.Repositories;
using SS.Services;

namespace SS.Product.Api.Tests
{
    public class ProductControllerTests
    {
        private Mock<IProductService> _mockedService;
        private Mock<IMapper> _mockedMapper;
        private ProductsController _productsController;

        [SetUp]
        public void SetUp()
        {
            _mockedMapper = new Mock<IMapper>();
            _mockedMapper.Setup(m => m.Map<Entities.Data.Product>(It.IsAny<ProductDto>())).Returns(new Entities.Data.Product());
            _mockedMapper.Setup(m => m.Map<Entities.Data.Product>(It.IsAny<ProductForCreationDto>())).Returns(new Entities.Data.Product());
            _mockedMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Entities.Data.Product>())).Returns(new ProductDto());
            _mockedMapper.Setup(m => m.Map(It.IsAny<ProductDto>(), It.IsAny<Entities.Data.Product>())).Returns(new Entities.Data.Product());

            _mockedService = new Mock<IProductService>();
            _mockedService.Setup(r => r.GetProductById(It.IsAny<Guid>())).Returns(new Entities.Data.Product());
            _mockedService.Setup(r => r.CreateProduct(It.IsAny<Entities.Data.Product>())).Returns(true);
            _mockedService.Setup(r => r.DeleteProduct(It.IsAny<Entities.Data.Product>())).Returns(true);
            _mockedService.Setup(r => r.UpdateProduct(It.IsAny<Entities.Data.Product>())).Returns(true);
            _mockedService.Setup(r => r.GetProducts()).Returns(new List<Entities.Data.Product>{ new Entities.Data.Product() , new Entities.Data.Product() });

            _productsController = new ProductsController(_mockedService.Object, _mockedMapper.Object);
        }

        [Test]
        public void Should_Call_Create_On_ProductService()
        {
            var actionResult = _productsController.Post(new ProductForCreationDto());
            var result = actionResult as CreatedAtRouteResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.RouteValues.Should().NotBeNull();

            _mockedService.Verify(r=>r.CreateProduct(It.IsAny<Entities.Data.Product>()), Times.Once);
        }

        [Test]
        public void Should_Call_Delete_On_ProductService()
        {
            var actionResult = _productsController.Delete(Guid.NewGuid());
            var result = actionResult as NoContentResult;

            result.Should().NotBeNull();

            _mockedService.Verify(r => r.DeleteProduct(It.IsAny<Entities.Data.Product>()), Times.Once);
        }

        [Test]
        public void Should_Call_Update_On_ProductService()
        {
            var actionResult = _productsController.Put(Guid.NewGuid(), new ProductDto());
            var result = actionResult as NoContentResult;

            result.Should().NotBeNull();

            _mockedService.Verify(r => r.UpdateProduct(It.IsAny<Entities.Data.Product>()), Times.Once);
        }

        [Test]
        public void Should_Call_GetProducts_On_ProductService()
        {
            var actionResult = _productsController.Get();
            var result = actionResult as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            _mockedService.Verify(r => r.GetProducts(), Times.Once);
        }

        [Test]
        public void Should_Call_GetProductById_On_ProductService()
        {
            var actionResult = _productsController.Get(Guid.NewGuid());
            var result = actionResult as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            _mockedService.Verify(r => r.GetProductById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
