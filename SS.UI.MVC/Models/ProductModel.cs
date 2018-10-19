using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SS.UI.MVC.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
