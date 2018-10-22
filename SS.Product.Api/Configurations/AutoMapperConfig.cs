using AutoMapper;
using SS.Product.Api.Dto.Product.Input;
using SS.Product.Api.Dto.Product.Output;

namespace SS.Product.Api.Configurations
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Data.Product, ProductForCreationDto>().ReverseMap();
            CreateMap<Entities.Data.Product, ProductForUpdateDto>().ReverseMap();
            CreateMap<Entities.Data.Product, ProductDto>().ReverseMap();
        }
    }
}