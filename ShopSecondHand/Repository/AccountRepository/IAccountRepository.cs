using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Data.ResponseModels.AuthenResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.AccountRepository
{
    public interface IAccountRepository 
    {
        Task<IEnumerable<GetAccountResponse>> GetAccount();
        Task<Account> CreateAccountWithWallet(CreateAccountRequest userRequest);
        //Task<CreateAccountResponse> CreateAccount(CreateAccountRequest userRequest);
        Task<UpdateAccountResponse> UpdateAccount(Guid id, UpdateAccountRequest request);
        void DeleteAccount(Guid id);
        Task<AccountWithWalletDTO> GetAccountWithWallet(Guid id);
        Task<GetAccountResponse> GetAccountById(Guid id);
       // Task<Account> GetByUserNameAndPassword(string userName, string password);
        // Task<List<Account>> GetAccountByBuildingId(Guid id);
        Task<IEnumerable<GetAccountResponse>> GetAccountByBuildingId(Guid id);
        Task<bool> AddBalanceAccount(Guid id,float money);

    }
}
