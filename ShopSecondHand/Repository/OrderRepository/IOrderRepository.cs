using ShopSecondHand.Data.RequestModels.OrderRequest;
using ShopSecondHand.Data.ResponseModels.OrderResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.OrderRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<GetOrderResponse>> GetOrder();
        Task<IEnumerable<GetOrderResponse>> GetOrderByAccountId(Guid id);
        Task<GetOrderWithTransactionResponse> GetOrderById(Guid id);
        Task<IEnumerable<GetOrderResponse>> GetOrderByPostId(Guid id);
        Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);
        Task<UpdateOrderResponse> UpdateOrder(Guid id, UpdateOrderRequest request);
        void Delete(Guid id);
    }
}
