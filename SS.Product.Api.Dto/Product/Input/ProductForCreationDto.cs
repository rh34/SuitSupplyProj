using System.ComponentModel.DataAnnotations;
using SS.Product.Api.Dto.Product.Output;

namespace SS.Product.Api.Dto.Product.Input
{
    public class ProductForCreationDto
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string PhotoUrl { get; set; }
        [Required]
        [Range(0.01, 20000)]
        public decimal Price { get; set; }
        [Required]
        [EnumDataType(typeof(CurrencyEnumDto), ErrorMessage = "Currency value doesn't exist within enum.")]
        public CurrencyEnumDto Currency { get; set; }
    }
}
