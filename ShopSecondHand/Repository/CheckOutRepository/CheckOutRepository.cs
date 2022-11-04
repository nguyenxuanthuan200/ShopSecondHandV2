using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.CheckOutRequest;
using ShopSecondHand.Data.RequestModels.OrderDetailRequest;
using ShopSecondHand.Data.RequestModels.OrderRequest;
using ShopSecondHand.Data.ResponseModels.OrderDetailResponse;
using ShopSecondHand.Data.ResponseModels.OrderResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.OrderDetailRepository;
using ShopSecondHand.Repository.OrderRepository;
using System.Linq;
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

            if (request.WalletId != request.AccountId) return false;
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
                    //check quantity
                    var getproduct = await dbContext.Products.SingleOrDefaultAsync(p => p.Id == product.Id);
                    if (getproduct.Quantity < product.Quantity)
                    {
                        return false;
                    }

                    order.PostId = product.Id;
                    order.Total = getproduct.Price * product.Quantity;
                    CreateOrderResponse response = await orderRepository.CreateOrder(order);
                    orderdeail.ProductId = product.Id;
                    orderdeail.OrderId = response.Id;
                    orderdeail.Quantity = product.Quantity;
                    orderdeail.Price =getproduct.Price;
                    await orderDetailRepository.CreateOrderDetail(orderdeail);
                    getproduct.Quantity=getproduct.Quantity-product.Quantity;
                    dbContext.Products.Update(getproduct);
                    await dbContext.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }
    }
}
