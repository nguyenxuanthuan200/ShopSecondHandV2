using ShopSecondHand.Data.RequestModels.CheckOutRequest;
using ShopSecondHand.Data.RequestModels.OrderDetailRequest;
using ShopSecondHand.Data.RequestModels.OrderRequest;
using ShopSecondHand.Data.ResponseModels.OrderDetailResponse;
using ShopSecondHand.Data.ResponseModels.OrderResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.OrderDetailRepository;
using ShopSecondHand.Repository.OrderRepository;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.CheckOutRepository
{
    public class CheckOutRepository : ICheckOutRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        public CheckOutRepository(ShopSecondHandContext dbContext,IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.dbContext = dbContext;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }
        public async Task<bool> CheckOut(CheckOutRequest request)
        {
            
            
            if (request.CartList != null)
            {
                CreateOrderRequest order = new CreateOrderRequest();
                //order.PostId = request.PostId;
                order.AccountId = request.AccountId;
                // order.Total = request.Total;
                order.WalletId = request.WalletId;
                order.Description = request.Description;
                order.TransactionType = request.TransactionType;

                
                CreateOrderDetailRequest orderdeail = new CreateOrderDetailRequest();
                foreach (var product in request.CartList)
                {
                    order.PostId = product.Id;
                    order.Total = product.Total;
                    CreateOrderResponse response = await orderRepository.CreateOrder(order);
                    orderdeail.ProductId = product.Id;
                    orderdeail.OrderId = response.Id;
                    orderdeail.Quantity = product.Quantity;
                    orderdeail.Price = product.Price;
                    await orderDetailRepository.CreateOrderDetail(orderdeail);
                }
                return true;
            }
            return false;
        }
    }
}
