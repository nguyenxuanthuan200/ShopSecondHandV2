using System;

namespace ShopSecondHand.Data.ResponseModels.PaymentResponse
{
    public class GetPaymentResponse
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public TimeSpan? PaymentTime { get; set; }
    }
}
