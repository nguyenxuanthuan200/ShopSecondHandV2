using ShopSecondHand.Data.RequestModels.OrderDetailRequest;
using ShopSecondHand.Data.ResponseModels.OrderDetailResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.OrderDetailRepository
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<GetOrderDetailResponse>> GetOrderDetail();
        Task<IEnumerable<GetOrderDetailResponse>> GetOrderDetailByOrderId(Guid id);
        Task<GetOrderDetailResponse> GetOrderDetailById(Guid id);
        Task<CreateOrderDetailResponse> CreateOrderDetail(CreateOrderDetailRequest request);
        Task<UpdateOrderDetailResponse> UpdateOrderDetail(Guid id, UpdateOrderDetailRequest request);
        void DeleteOrderDetail(Guid id);
    }
}
