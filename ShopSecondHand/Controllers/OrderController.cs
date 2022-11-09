using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.OrderRequest;
using ShopSecondHand.Data.ResponseModels.OrderResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.OrderRepository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderRepository orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            try
            {
                var result = await orderRepository.GetOrder();
                if (result == null)
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                var result = await orderRepository.GetOrderById(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("posts")]
        public async Task<IActionResult> GetOrderByPostId(Guid id)
        {
            try
            {
                var result = await orderRepository.GetOrderByPostId(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);


            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("users")]
        public async Task<IActionResult> GetOrderByAccountId(Guid id)
        {
            try
            {
                var result = await orderRepository.GetOrderByAccountId(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var create = await orderRepository.CreateOrder(request);
                if (create == null)
                {
                    return CustomResult("Balance k du", HttpStatusCode.Accepted);
                }
                return CustomResult("Success", create, HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var update = await orderRepository.UpdateOrder(id, request);

                if (update == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Success", update, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            try
            {
                var delete = orderRepository.GetOrderById(id);

                if (delete == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                orderRepository.Delete(id);
                return CustomResult("Success", HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
    }
}
