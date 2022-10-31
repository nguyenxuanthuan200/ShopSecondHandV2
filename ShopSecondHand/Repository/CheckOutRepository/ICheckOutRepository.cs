using ShopSecondHand.Data.RequestModels.CheckOutRequest;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.CheckOutRepository
{
    public interface ICheckOutRepository
    {
        Task<bool> CheckOut(CheckOutRequest request);
    }
}
