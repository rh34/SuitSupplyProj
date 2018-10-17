using System;

namespace SS.Entities.Data
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public CurrencyEnum Currency { get; set; }
    }
}
