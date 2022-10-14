using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.OrderDetailRequest;
using ShopSecondHand.Data.ResponseModels.OrderDetailResponse;
using ShopSecondHand.Repository.OrderDetailRepository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/orderdetails")]
    [ApiController]
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderDetail()
        {
            try
            {
                var result = await orderDetailRepository.GetOrderDetail();
                if (result == null)
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(Guid id)
        {
            try
            {
                var result = await orderDetailRepository.GetOrderDetailById(id);
                if (result == null)
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [HttpGet("order")]
        public async Task<IActionResult> GetOrderDetailByOrderId(Guid id)
        {
            try
            {
                var result = await orderDetailRepository.GetOrderDetailByOrderId(id);
                if (result == null)
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDetailRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var create = await orderDetailRepository.CreateOrderDetail(request);
                if (create == null)
                {
                    return CustomResult("Order da ton tai", HttpStatusCode.Accepted);
                }
                return CustomResult("Success", create, HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(Guid id, UpdateOrderDetailRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var update = await orderDetailRepository.UpdateOrderDetail(id, request);

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
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(Guid id)
        {
            try
            {
                var delete = orderDetailRepository.GetOrderDetailById(id);

                if (delete == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                orderDetailRepository.DeleteOrderDetail(id);
                return CustomResult("Success", HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
    }
}
