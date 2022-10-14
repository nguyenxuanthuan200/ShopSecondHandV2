using AutoMapper;

namespace ShopSecondHand.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Models.Order, Data.ResponseModels.OrderResponse.GetOrderResponse>().ReverseMap();
            CreateMap<Models.Order, Data.ResponseModels.OrderResponse.CreateOrderResponse>().ReverseMap();
            CreateMap<Models.Order, Data.ResponseModels.OrderResponse.UpdateOrderResponse>().ReverseMap();

            CreateMap<Models.Transaction, Data.ResponseModels.OrderResponse.CreateOrderResponse>().ReverseMap();
        }
    }
}
