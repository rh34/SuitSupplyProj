using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SS.Repositories;
using SS.Services;

namespace SS.Product.Api.Tests
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockedRepository;
        private ProductService _productService;

        [SetUp]
        public void SetUp()
        {
            _mockedRepository = new Mock<IProductRepository>();
            _mockedRepository.Setup(r => r.GetProductById(It.IsAny<Guid>())).Returns(new Entities.Data.Product());
            _mockedRepository.Setup(r => r.CreateProduct(It.IsAny<Entities.Data.Product>())).Returns(true);
            _mockedRepository.Setup(r => r.DeleteProduct(It.IsAny<Entities.Data.Product>())).Returns(true);
            _mockedRepository.Setup(r => r.UpdateProduct(It.IsAny<Entities.Data.Product>())).Returns(true);
            _mockedRepository.Setup(r => r.GetProducts()).Returns(new List<Entities.Data.Product>{ new Entities.Data.Product() , new Entities.Data.Product() });

            _productService = new ProductService(_mockedRepository.Object);
        }

        [Test]
        public void Should_Call_Create_On_ProductService()
        {
            var result = _productService.CreateProduct(new Entities.Data.Product());
            result.Should().Be(true);
            _mockedRepository.Verify(r=>r.CreateProduct(It.IsAny<Entities.Data.Product>()), Times.Once);
        }

        [Test]
        public void Should_Call_Delete_On_ProductService()
        {
            var result = _productService.DeleteProduct(new Entities.Data.Product());
            result.Should().Be(true);
            _mockedRepository.Verify(r => r.DeleteProduct(It.IsAny<Entities.Data.Product>()), Times.Once);
        }

        [Test]
        public void Should_Call_Update_On_ProductService()
        {
            var result = _productService.UpdateProduct(new Entities.Data.Product());
            result.Should().Be(true);
            _mockedRepository.Verify(r => r.UpdateProduct(It.IsAny<Entities.Data.Product>()), Times.Once);
        }

        [Test]
        public void Should_Call_GetProducts_On_ProductService()
        {
            var result = _productService.GetProducts();
            result.Count().Should().Be(2);
            _mockedRepository.Verify(r => r.GetProducts(), Times.Once);
        }

        [Test]
        public void Should_Call_GetProductById_On_ProductService()
        {
            var result = _productService.GetProductById(Guid.NewGuid());
            result.Should().NotBe(null);
            _mockedRepository.Verify(r => r.GetProductById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
