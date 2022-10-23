using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.OrderDetailRequest;
using ShopSecondHand.Data.ResponseModels.OrderDetailResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.OrderDetailRepository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;

        public OrderDetailRepository(ShopSecondHandContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CreateOrderDetailResponse> CreateOrderDetail(CreateOrderDetailRequest request)
        {
            //var create = await dbContext.OrderDetails
            //       .SingleOrDefaultAsync(p => p.Name.Equals(request.Name));
            //if (create != null)
            //    return null;
            OrderDetail a = new OrderDetail();
            {
                a.Id = Guid.NewGuid();
                a.ProductId = request.ProductId;
                a.OrderId = request.OrderId;
                a.Price = request.Price;
                a.Quantity = request.Quantity;
            };
            await dbContext.OrderDetails.AddAsync(a);
            await dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreateOrderDetailResponse>(a);
            return re;
        }

        public void DeleteOrderDetail(Guid id)
        {
            var delete = dbContext.OrderDetails
                .SingleOrDefault(p => p.Id == id);
            if (delete == null)
            {
                // throw new Exception("This Building is unavailable!");
            }
            dbContext.OrderDetails.Remove(delete);
            dbContext.SaveChanges();
        }

        public async Task<IEnumerable<GetOrderDetailResponse>> GetOrderDetail()
        {
            var get = await dbContext.OrderDetails.ToListAsync();
            IEnumerable<GetOrderDetailResponse> result = get.Select(
                x =>
                {
                    return new GetOrderDetailResponse()
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        OrderId = x.OrderId,
                        Price = x.Price,
                        Quantity = x.Quantity
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<GetOrderDetailResponse> GetOrderDetailById(Guid id)
        {
            var getById = await dbContext.OrderDetails
              .FirstOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var re = new GetOrderDetailResponse()
                {
                    Id = getById.Id,
                    ProductId = getById.ProductId,
                    OrderId = getById.OrderId,
                    Price = getById.Price,
                    Quantity = getById.Quantity
                };
                return re;
            }
            return null;
        }

        public async Task<IEnumerable<GetOrderDetailResponse>> GetOrderDetailByOrderId(Guid id)
        {
            var getByPostId = await dbContext.OrderDetails
                .FirstOrDefaultAsync(p => p.OrderId == id);
            if (getByPostId == null) return null;

            var userByBuilding = await dbContext.OrderDetails
                .Where(p => p.OrderId == id).ToListAsync();

            IEnumerable<GetOrderDetailResponse> result = userByBuilding.Select(
                x =>
                {
                    return new GetOrderDetailResponse()
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        OrderId = x.OrderId,
                        Price = x.Price,
                        Quantity = x.Quantity
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<UpdateOrderDetailResponse> UpdateOrderDetail(Guid id, UpdateOrderDetailRequest request)
        {
            var up = await dbContext.OrderDetails.SingleOrDefaultAsync(c => c.Id == id);
            if (id != request.Id) return null;
            if (up == null) return null;

            up.Id = request.Id;
            up.ProductId = request.ProductId;
            up.Price = request.Price;
            up.Quantity = request.Quantity;
            up.OrderId = request.OrderId;
            dbContext.OrderDetails.Update(up);
            await dbContext.SaveChangesAsync();

            var upResult = _mapper.Map<UpdateOrderDetailResponse>(up);
            return upResult;
        }
    }
}
