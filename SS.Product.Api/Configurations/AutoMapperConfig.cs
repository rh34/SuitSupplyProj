using AutoMapper;
using SS.Product.Api.Models;

namespace SS.Product.Api.Configurations
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Data.Product, ProductForCreationDto>().ReverseMap();
        }
    }
}