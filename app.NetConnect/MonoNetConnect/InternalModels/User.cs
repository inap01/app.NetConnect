using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonoNetConnect.Cache;
using Newtonsoft.Json;

namespace MonoNetConnect.InternalModels
{
    public class User: BaseProperties, IApiModels
    {        
        public static String UserApiPath = @"api.php/auth/Login";

        [JsonProperty("first_name")]
        public String FirstName { get; set; }
        [JsonProperty("last_name")]
        public String LastName { get; set; }
        [JsonProperty("email")]
        public String EMail { get; set; }
        [JsonProperty("nickname")]
        public String NickName { get; set; }
        [JsonProperty("steam_id")]
        public String SteamID { get; set; }
        [JsonProperty("battle_tag")]
        public String BattleTag { get; set; }
        [JsonProperty("registered_since")]
        public DateTime RegisteredSince { get; set; }
        [JsonProperty("is_admin")]
        public Boolean IsAdmin { get; set; }

        public String ApiPath()
        {
            return UserApiPath.Replace("{/id}", $"/{ID}");
        }

        public string GetImageDirectoryPath()
        {
            throw new NotImplementedException();
        }

        public bool IsClassWithImage()
        {
            return false;
        }
    }
}