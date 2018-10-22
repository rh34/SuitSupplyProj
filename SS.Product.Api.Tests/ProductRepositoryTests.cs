using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SS.Entities.Data;
using SS.Repositories;

namespace SS.Product.Api.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private ProductRepository _repository;
        SuitsupplyDbContext _context;
        //Mock<GenericRepository<Entities.Data.Product>> _mockedGenericRepo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SuitsupplyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SuitsupplyDbContext(options);

            Seed(_context);
            //_mockedGenericRepo = new Mock<GenericRepository<Entities.Data.Product>>();
            _repository = new ProductRepository(_context);


        }

        public void Seed(SuitsupplyDbContext dbContext)
        {
            dbContext.Products.AddRange(
                new Entities.Data.Product()
                {
                    Created = new DateTime(2018, 05, 03, 22, 50, 59),
                    LastUpdated = new DateTime(2018, 05, 03, 22, 50, 59),
                    Currency = CurrencyEnum.EUR,
                    Price = 2.42m,
                    Id = Guid.Parse("9cd4b46f-164a-4223-96ac-48fccfa0ec50"),
                    PhotoUrl = "http://....",
                    Name = "Product 1"
                }, 
                new Entities.Data.Product()
                {
                    Created = new DateTime(2018, 05, 01, 22, 50, 59),
                    LastUpdated = new DateTime(2018, 05, 04, 22, 50, 59),
                    Currency = CurrencyEnum.USD,
                    Price = 4.29m,
                    Id = Guid.Parse("dba627a6-bbc1-4a3b-a0f6-f268631bbc35"),
                    PhotoUrl = "http://....",
                    Name = "Product 2"
                }, 
                new Entities.Data.Product()
                {
                    Created = new DateTime(2018, 06, 01, 22, 50, 59),
                    LastUpdated = new DateTime(2018, 06, 04, 22, 50, 59),
                    Currency = CurrencyEnum.CHF,
                    Price = 929.99m,
                    Id = Guid.Parse("9b8d0bb2-b254-44de-ae88-ca0c2f254e9b"),
                    PhotoUrl = "http://....",
                    Name = "Product 3"
                });
            dbContext.SaveChanges();
        }

        [Test]
        public void Given3ProductsInDb_When_GetAllIsCalled_Then_Returns_3Items()
        {
            var result = _repository.GetProducts();

            result.Count().Should().Be(3);
        }

        [Test]
        [Ignore("no null case mocking done yet")]
        public void GivenNoItemsInDb_When_GetAllIsCalled_Then_Returns_null()
        {
            //_mockedGenericRepo.Setup(d => d.GetAll()).Returns((IQueryable<Entities.Data.Product>)null);
            var result = _repository.GetProducts();

            result.Should().BeEmpty();
        }
    }
}
