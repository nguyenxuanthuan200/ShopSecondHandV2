using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.Common;
using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Repository.AccountRepository;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.AuthenRepository;
using System;
using System.Net;
using System.Threading.Tasks;
using ShopSecondHand.Data.RequestModels.AuthenRequest;

namespace ShopSecondHand.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository accountRepository;
        private readonly IAuthenRepository authenRepository;
        public AccountController(IAccountRepository accountRepository, IAuthenRepository authenRepository)
        {
            {
                this.accountRepository = accountRepository;
                this.authenRepository=authenRepository;
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccontWithWallet(Guid id)
        {
            try
            {
                var result = await accountRepository.GetAccountWithWallet(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            try
            {
                var result = await accountRepository.GetAccount();
                if (result == null)
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAccountRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }

                var update = await accountRepository.UpdateAccount(id, request);

                if (update == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                //var result = _mapper.Map<CreateAccountResponse>(update);
                return CustomResult("Success", update, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            try
            {
                var delete = accountRepository.GetAccountById(id);
                if (delete == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }
                accountRepository.DeleteAccount(id);
                return CustomResult("Success", HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("buildings")]
        public async Task<IActionResult> GetAccountByBuildingId(Guid id)
        {
            try
            {
                var result = await accountRepository.GetAccountByBuildingId(id);
                if (result == null)
                {
                    return CustomResult("Not Found", HttpStatusCode.NotFound);
                }

                return CustomResult("Success", result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);

            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateAccountWithWallet(CreateAccountRequest request)
        {
            try
            {
                if (request == null)
                {
                    return CustomResult("Cu Phap Sai", HttpStatusCode.BadRequest);
                }
                var create = await accountRepository.CreateAccountWithWallet(request);
                if (create == null)
                {
                    return CustomResult("Account da ton tai", HttpStatusCode.Accepted);
                }
                var result = await authenRepository.GenerateToken(create);
                return CustomResult("Success", result, HttpStatusCode.Created);

            }
            catch (Exception)
            {
                return CustomResult("Fail", HttpStatusCode.InternalServerError);


            }
        }
       
    }
}
