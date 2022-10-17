using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.SearchRequest;
using ShopSecondHand.Repository.SearchRepository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchController : BaseController
    {
        private readonly ISearchRepository searchRepository;
        public SearchController(ISearchRepository searchRepository)
        {
            {
                this.searchRepository = searchRepository;
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> SearchFilter([FromQuery] SearchRequest payload)
        {
            try
            {

                var result = await searchRepository.SearchFilter(payload);

                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, HttpStatusCode.OK);
            }catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
    }
}
