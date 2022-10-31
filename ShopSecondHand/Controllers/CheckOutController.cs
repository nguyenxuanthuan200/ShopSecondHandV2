using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.CheckOutRequest;
using ShopSecondHand.Repository.CheckOutRepository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/checkouts")]
    [ApiController]
    public class CheckOutController : BaseController
    {
        private readonly ICheckOutRepository checkOutRepository;
        public CheckOutController(ICheckOutRepository checkOutRepository)
        {
            {
                this.checkOutRepository = checkOutRepository;
            }

        }
        [HttpPost]
        public async Task<IActionResult> CheckOut([FromBody]CheckOutRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var create = await checkOutRepository.CheckOut(request);
                if (!create)
                {
                    return CustomResult("CheckOut Fails", HttpStatusCode.Accepted);
                }
                return CustomResult("Success", create, HttpStatusCode.Created);

            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);


            }
        }


    }
}
