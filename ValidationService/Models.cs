
using Newtonsoft.Json;

namespace ValidationService
{
    public class Models
    {
        public class TokenValidation
        {
            [JsonProperty("valid")]
            public bool Valid { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("user")]
            public string User { get; set; }
        }

        public class ValidationModel
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
            public int TokenValidationType { get; set; }
        }
    }
}
