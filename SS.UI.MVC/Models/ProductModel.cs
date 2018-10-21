using System;
using System.ComponentModel.DataAnnotations;

namespace SS.UI.MVC.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string PhotoUrl { get; set; }
        [Required]
        [Range(0.01, 20000)]
        public decimal Price { get; set; }
        [Required]
        [EnumDataType(typeof(CurrencyEnum), ErrorMessage = "Currency value doesn't exist within enum.")]
        public CurrencyEnum Currency { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
