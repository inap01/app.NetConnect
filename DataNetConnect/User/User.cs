using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DataNetConnect.ViewModels.User
{
    public class UserViewModel_Frontend : BaseViewModel
    {
        [JsonProperty("first_name")]
        public String FirstName { get; set; }

        [JsonProperty("last_name")]
        public String LastName { get; set; }

        [JsonProperty("nickname")]
        public String Nickname { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("steam_id")]
        public String SteamID { get; set; }

        [JsonProperty("battle_tag")]
        public String BattleTag { get; set; }

        [JsonProperty("registered_since")]
        public DateTime RegisteredSince { get; set; }

        [JsonProperty("newsletter")]
        public Boolean Newsletter { get; set; }

        [JsonProperty("is_admin")]
        public String IsAdmin { get; set; }

        [JsonProperty("is_vorstand")]
        public String IsVorstand { get; set; }

        [JsonProperty("image")]
        public String Image { get; set; }

        public UserViewModel_Frontend()
        {

        }
    }
}