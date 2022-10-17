using System;

namespace ShopSecondHand.Data.ResponseModels.AuthenResponse
{
    public class Token
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public string JwtToken { get; set; }
    }
}
