using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using VoiceAPI.Models.Responses.Transaction;

namespace ShopSecondHand.Data.ResponseModels.WalletResponse
{
    public class WalletWithTransactionsDTO
    {
        public Guid Id { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal LockedBalance { get; set; }
        
        public string DepositCode { get; set; }

       // public List<TransactionHistoryDTO> TransactionHistories { get; set; }
    }
}
