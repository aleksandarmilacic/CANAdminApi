using Newtonsoft.Json;

namespace CANAdminApi.Services.Extensions
{
    public class ErrorDetails
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}