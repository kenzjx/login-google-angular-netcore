using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.Auth
{
    public class GoogleUserRequest
    {
        public const string PROVIDER = "google";

       
        [Required]
       
        [JsonProperty("idToken")]
        public string IdToken {set;get;}

    }
}