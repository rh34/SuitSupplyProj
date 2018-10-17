﻿using System;

namespace SS.Product.Api.Models
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public CurrencyEnumDto Currency { get; set; }
    }
}
