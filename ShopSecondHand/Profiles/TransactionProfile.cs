using AutoMapper;
using ShopSecondHand.Data.ResponseModels.TransactionResponse;

namespace ShopSecondHand.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Models.Transaction, TransactionDTO>().ReverseMap();
        }
    }
}
