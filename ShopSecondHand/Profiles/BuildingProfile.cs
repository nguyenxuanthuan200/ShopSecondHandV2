using AutoMapper;

namespace ShopSecondHand.Profiles
{
    public class BuildingProfile : Profile
    {
        public BuildingProfile()
        {
            CreateMap<Models.Building, Data.ResponseModels.BuildingResponse.GetBuildingResponse>().ReverseMap();
            CreateMap<Models.Building, Data.ResponseModels.BuildingResponse.CreateBuildingResponse >().ReverseMap();
            CreateMap<Models.Building, Data.ResponseModels.BuildingResponse.UpdateBuildingResponse>().ReverseMap();
        }
    }
}
