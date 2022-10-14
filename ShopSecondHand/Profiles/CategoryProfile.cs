using AutoMapper;

namespace ShopSecondHand.Profiles
{
    public class CategoryProfile : Profile
    { 
        public CategoryProfile()
        {
            CreateMap<Models.Category, Data.ResponseModels.CategoryResponse.GetCategoryResponse>().ReverseMap();
            CreateMap<Models.Category, Data.ResponseModels.CategoryResponse.CreateCategoryResponse>().ReverseMap();
            CreateMap<Models.Category, Data.ResponseModels.CategoryResponse.UpdateCategoryResponse>().ReverseMap();
        }
    }
}

