using System;
using System.Collections.Generic;
using System.Linq;
using SS.Entities.Data;

namespace SS.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(SuitsupplyDbContext dbContext) : base(dbContext)
        {
        }
        public Product GetProductById(Guid id)
        {
            return FindBy(p => p.Id == id).FirstOrDefault();
        }

        public bool UpdateProduct(Product product)
        {
            Edit(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            Delete(product);
            return Save();
        }

        public bool CreateProduct(Product product)
        {
            Add(product);
            return Save();
        }

        public IEnumerable<Product> GetProducts()
        {
            return GetAll();
        }
    }
}
