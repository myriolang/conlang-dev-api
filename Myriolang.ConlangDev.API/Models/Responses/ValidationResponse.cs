namespace Myriolang.ConlangDev.API.Models.Responses
{
    public class ValidationResponse
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public bool Valid { get; set; }
        public string Message { get; set; }
    }
}