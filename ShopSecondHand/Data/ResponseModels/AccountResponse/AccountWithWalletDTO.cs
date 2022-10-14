﻿using ShopSecondHand.Data.ResponseModels.WalletResponse;
using System;

namespace ShopSecondHand.Data.ResponseModels.AccountResponse
{
    public class AccountWithWalletDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string AvatarUrl { get; set; }
        public Guid? BuildingId { get; set; }
        public WalletDTO Wallet { get; set; }
    }
}
