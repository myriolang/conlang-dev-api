namespace Myriolang.ConlangDev.API.Models.Responses
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public Profile Profile { get; set; }
    }
}