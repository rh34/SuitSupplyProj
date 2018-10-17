using System;
using System.ComponentModel.DataAnnotations;

namespace SS.Entities.Data
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }

        [Required]
        public CurrencyEnum Currency { get; set; }
    }
}
