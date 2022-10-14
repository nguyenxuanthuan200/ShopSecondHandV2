using System;

namespace ShopSecondHand.Data.RequestModels.PaymentRequest
{
    public class CreatePaymentRequest
    {
        public Guid? OrderId { get; set; }
        public TimeSpan? PaymentTime { get; set; }
    }
}
