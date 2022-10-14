using AutoMapper;

namespace ShopSecondHand.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, Data.ResponseModels.ProductResponse.GetProductResponse>().ReverseMap();
            CreateMap<Models.Product, Data.ResponseModels.ProductResponse.CreateProductResponse>().ReverseMap();
            CreateMap<Models.Product, Data.ResponseModels.ProductResponse.UpdateProductResponse>().ReverseMap();
        }
    }
}
