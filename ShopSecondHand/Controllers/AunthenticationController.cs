using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopSecondHand.Data.RequestModels.AuthenRequest;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.AuthenRepository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.AuthenticationController
{
    [Route("api/authen")]
    [ApiController]
    public class AunthenticationController : BaseController
    {
        public IConfiguration _configuration;
        private readonly ShopSecondHandContext _context;
        private readonly IAuthenRepository _authenRepository;

        public AunthenticationController(IConfiguration configuration, ShopSecondHandContext context, IAuthenRepository authenRepository)
        {
            _configuration = configuration;
            _context = context;
            _authenRepository = authenRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var account = await _authenRepository.LoginByUserNameAndPassword(request);
                if (account == null)
                {
                    return CustomResult("Username Or Password wrong!!", HttpStatusCode.NotFound);
                }
                var result = await _authenRepository.GenerateToken(account);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
    }
}
