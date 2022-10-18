using AutoMapper;

namespace ShopSecondHand.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Models.Post, Data.ResponseModels.PostResponse.CreatePostResponse>().ReverseMap();
            CreateMap<Models.Post, Data.ResponseModels.PostResponse.UpdatePostResponse>().ReverseMap();
            CreateMap<Models.Post, Data.ResponseModels.PostResponse.GetPostWithProductResponse>().ReverseMap();
            CreateMap<Models.Post, Data.ResponseModels.PostResponse.GetPostResponse>().ReverseMap();
            CreateMap< Data.ResponseModels.ProductResponse.UpdateProductResponse, Data.ResponseModels.PostResponse.UpdatePostResponse>().ReverseMap();
        }
    }
}
