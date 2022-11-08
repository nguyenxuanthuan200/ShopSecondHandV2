using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.OrderRequest;
using ShopSecondHand.Data.ResponseModels.OrderResponse;
using ShopSecondHand.Data.ResponseModels.PostResponse;
using ShopSecondHand.Data.ResponseModels.TransactionResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.OrderDetailRepository;
using ShopSecondHand.Repository.PostRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;
        private readonly IPostRepository postRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrderRepository(ShopSecondHandContext dbContext, IMapper mapper, IPostRepository postRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this.postRepository = postRepository;
            this.orderDetailRepository = orderDetailRepository;
        }
        public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
        {
            var checkAccount = await dbContext.Accounts.Where(p => p.Id == request.AccountId && p.Status==true).SingleOrDefaultAsync();
            if (checkAccount==null) return null;
            if (request.TransactionType.ToUpper().Equals("VI"))
            {
                var balance = await dbContext.Wallets.Where(p => p.Id == request.WalletId).SingleOrDefaultAsync();
                if (balance.Balance >= request.Total)//du tien
                {
                    balance.Balance = balance.Balance - request.Total;
                    var baipostcuanguoiban = await dbContext.Posts.Where(p => p.Id == request.PostId).SingleOrDefaultAsync();
                    var vitiencuanguoiban = await dbContext.Wallets.Where(p => p.Id == baipostcuanguoiban.AccountId).SingleOrDefaultAsync();
                    vitiencuanguoiban.Balance = vitiencuanguoiban.Balance + request.Total;
                    dbContext.Wallets.Update(balance);
                    dbContext.Wallets.Update(vitiencuanguoiban);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    return null;
                }

            }
            var id = Guid.NewGuid();
            Order order = new Order();
            {
                order.Id = id;
                order.PostId = request.PostId;
                order.AccountId = request.AccountId;
                order.Total = request.Total;
            };
            Transaction transaction = new Transaction();
            {
                transaction.Id = id;
                transaction.TransactionTime = DateTime.Now;
                transaction.Status = 1;
                transaction.TransactionType = request.TransactionType;
                transaction.WalletId = request.WalletId;
                transaction.Description = request.Description;
            }
            await dbContext.Orders.AddAsync(order);
            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreateOrderResponse>(order);
            re.TransactionType = transaction.TransactionType;
            re.TransactionTime = transaction.TransactionTime;
            re.Description = transaction.Description;
            re.WalletId = transaction.WalletId;
            return re;
        }

        public async void Delete(Guid id)
        {
            var deBuilding = dbContext.Orders
                .SingleOrDefaultAsync(p => p.Id == id);
            if (deBuilding == null)
            {
                // throw new Exception("This Building is unavailable!");
            }
            dbContext.Orders.Remove(await deBuilding);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetOrderResponse>> GetOrder()
        {
            var order = await dbContext.Orders.ToListAsync();
            IEnumerable<GetOrderResponse> result = order.Select(
               x =>
               {
                   return new GetOrderResponse()
                   {
                       Id = x.Id,
                       PostId = x.PostId,
                       AccountId = x.AccountId,
                       Total = x.Total,
                   };
               }
                ).ToList();
            return result;
        }

        public async Task<GetOrderWithTransactionResponse> GetOrderById(Guid id)
        {
            var getById = await dbContext.Orders
                .SingleOrDefaultAsync(p => p.Id == id);
            var transaction = await dbContext.Transactions.SingleOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var Transaction = _mapper.Map<TransactionDTO>(transaction);
                var re = new GetOrderWithTransactionResponse()
                {
                    Id = getById.Id,
                    PostId = getById.PostId,
                    AccountId = getById.AccountId,
                    Total = getById.Total,
                    Transaction = Transaction
                };
                return re;
            }
            return null;
        }

        public async Task<IEnumerable<GetOrderResponse>> GetOrderByPostId(Guid id)
        {
            var getByPostId = await dbContext.Orders
                .FirstOrDefaultAsync(p => p.PostId == id);
            //Guid idBuilding;
            if (getByPostId == null)
                return null;


            var userByBuilding = await dbContext.Orders
                .Where(p => p.PostId == id).ToListAsync();

            IEnumerable<GetOrderResponse> result = userByBuilding.Select(
                x =>
                {
                    return new GetOrderResponse()
                    {
                        Id = x.Id,
                        PostId = x.PostId,
                        AccountId = x.AccountId,
                        Total = x.Total,
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<IEnumerable<GetOrderTransactionPostResponse>> GetOrderByAccountId(Guid id)
        {
            var getByPostId = await dbContext.Orders
                .FirstOrDefaultAsync(p => p.AccountId == id);
            if (getByPostId == null)
                return null;


            var listOrder = await dbContext.Orders
                .Where(p => p.AccountId == id).ToListAsync();
            List< GetOrderTransactionPostResponse > list=new List<GetOrderTransactionPostResponse>();
            foreach (var order in listOrder)
            {
                GetPostWithProductResponse post= await postRepository.GetPostById((Guid)order.PostId);
                var transaction = await dbContext.Transactions.SingleOrDefaultAsync(p => p.Id == order.Id);
                var orderdetail= await orderDetailRepository.GetOrderDetailByOrderId(order.Id);
                GetOrderTransactionPostResponse orderr = new GetOrderTransactionPostResponse();
                var Transaction = _mapper.Map<TransactionDTO>(transaction);
                orderr.Id = order.Id;
                orderr.Total = order.Total;
                orderr.Post = post;
                foreach (var item in orderdetail)
                {
                    orderr.Quantity = item.Quantity;
                }
                orderr.Transaction = Transaction;
                list.Add(orderr);

            }
            //IEnumerable<GetOrderResponse> result = listOrder.Select(
            //     x =>
            //    {
            //        //   var getTransaction = await dbContext.Transactions
            //        //.SingleOrDefaultAsync(p => p.Id == x.Id);
            //        return new GetOrderResponse()
            //        {
            //            Id = x.Id,
            //            PostId = x.PostId,
            //            AccountId = x.AccountId,
            //            Total = x.Total
            //        };
            //    }
            //    ).ToList();
            return list;
        }

        public async Task<UpdateOrderResponse> UpdateOrder(Guid id, UpdateOrderRequest request)
        {
            var up = await dbContext.Orders.SingleOrDefaultAsync(c => c.Id == id);
            if (id != request.Id) return null;
            if (up == null) return null;

            //up.AccountId = request.AccountId;
            //up.PostId = request.PostId;
            up.Total = request.Total;
            dbContext.Orders.Update(up);
            await dbContext.SaveChangesAsync();

            var update = _mapper.Map<UpdateOrderResponse>(up);
            return update;
        }
    }
}
