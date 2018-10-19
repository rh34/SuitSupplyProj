using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SS.UI.MVC.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
      //  [format]
        public decimal Price { get; set; }
        [Required]
        public CurrencyEnum Currency { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
