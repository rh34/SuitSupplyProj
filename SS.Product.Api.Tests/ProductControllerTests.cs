using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SS.Product.Api.Controllers;
using SS.Product.Api.Dto.Product.Input;
using SS.Product.Api.Dto.Product.Output;
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
            _mockedMapper.Setup(m => m.Map<Entities.Data.Product>(It.IsAny<ProductForUpdateDto>())).Returns(new Entities.Data.Product());
            _mockedMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Entities.Data.Product>())).Returns(new ProductDto());
            _mockedMapper.Setup(m => m.Map(It.IsAny<ProductDto>(), It.IsAny<Entities.Data.Product>())).Returns(new Entities.Data.Product());
            _mockedMapper.Setup(m => m.Map(It.IsAny<ProductForUpdateDto>(), It.IsAny<Entities.Data.Product>())).Returns(new Entities.Data.Product());

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
            var actionResult = _productsController.Put(Guid.NewGuid(), new ProductForUpdateDto());
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
        public void Given_NoProducts_InRepo_GetProducts_Should_ReturnNotFound()
        {
            _mockedService.Setup(p => p.GetProducts()).Returns((IEnumerable<Entities.Data.Product>) null);
            var actionResult = _productsController.Get();
            var result = actionResult as NotFoundResult;

            result.Should().NotBeNull();

            _mockedService.Verify(r => r.GetProducts(), Times.Once);
            _mockedMapper.Verify(r => r.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Entities.Data.Product>>()), Times.Never);
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

        [Test]
        public void Given_NoMatch_InRepo_GetProductById_Should_ReturnNotFound()
        {
            _mockedService.Setup(p => p.GetProductById(It.IsAny<Guid>())).Returns((Entities.Data.Product)null);
            var actionResult = _productsController.Get(Guid.NewGuid());
            var result = actionResult as NotFoundResult;

            result.Should().NotBeNull();

            _mockedService.Verify(r => r.GetProductById(It.IsAny<Guid>()), Times.Once);
            _mockedMapper.Verify(r => r.Map<ProductDto>(It.IsAny<Entities.Data.Product>()), Times.Never);
        }
    }
}
