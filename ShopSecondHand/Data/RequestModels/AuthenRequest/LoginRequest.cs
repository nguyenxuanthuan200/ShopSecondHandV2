namespace ShopSecondHand.Data.RequestModels.AuthenRequest
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginRequest(string us, string pw)
        {
            this.UserName = us;
            this.Password = pw;
        }
    }
  

}
