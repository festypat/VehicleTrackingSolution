namespace Helper.Layer.Identity.Dto.Response
{
    public class LoginResponse
    {
        public string ResponseCode { get; set; } = "01";
        public string Message { get; set; } = "Invalid Username/Password";
        public TokenResponse tokenResponse { get; set; }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
