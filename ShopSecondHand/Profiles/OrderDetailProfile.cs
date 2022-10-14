using AutoMapper;

namespace ShopSecondHand.Profiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<Models.OrderDetail, Data.ResponseModels.OrderDetailResponse.GetOrderDetailResponse>().ReverseMap();
            CreateMap<Models.OrderDetail, Data.ResponseModels.OrderDetailResponse.CreateOrderDetailResponse>().ReverseMap();
            CreateMap<Models.OrderDetail, Data.ResponseModels.OrderDetailResponse.UpdateOrderDetailResponse>().ReverseMap();
        }
    }
}
