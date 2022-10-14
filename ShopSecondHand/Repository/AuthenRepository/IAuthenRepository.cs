using ShopSecondHand.Data.RequestModels.AuthenRequest;
using ShopSecondHand.Data.ResponseModels.AuthenResponse;
using ShopSecondHand.Models;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.AuthenRepository
{
    public interface IAuthenRepository
    {
        Task<Token> GenerateToken(Account account);
        Task<Account> LoginByUserNameAndPassword(LoginRequest payload);
    }
}
