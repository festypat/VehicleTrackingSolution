namespace Helper.Layer.Identity.Dto.Response
{
    public class ApiResponse
    {
        public string ResponseCode { get; set; } = "01";
        public string Message { get; set; } = "Failed";
        public object Data { get; set; }
    }
}
