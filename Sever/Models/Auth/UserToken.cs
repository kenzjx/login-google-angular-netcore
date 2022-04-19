using Newtonsoft.Json;

namespace Server.Auth
{
    public class UserToken
    {
        [Newtonsoft.Json.JsonProperty("userId")]
        public string UserId {set;get;}

        [JsonProperty("email")]
        public string Email {set;get;}

        [JsonProperty("token")]
        public string Token {set;get;}

        [JsonProperty("expires")]
        public DateTime Expires {set;get;}
    }
}