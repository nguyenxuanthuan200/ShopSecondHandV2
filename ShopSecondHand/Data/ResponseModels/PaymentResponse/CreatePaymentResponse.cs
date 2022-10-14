using System;

namespace ShopSecondHand.Data.ResponseModels.PaymentResponse
{
    public class CreatePaymentResponse
    {
        public Guid? OrderId { get; set; }
        public TimeSpan? PaymentTime { get; set; }
    }
}
