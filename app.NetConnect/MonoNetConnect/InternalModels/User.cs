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
        public static String UserApiPath = @"api.php/app/User{/id}";

        [JsonProperty("FirstName")]
        public String FirstName { get; set; }
        [JsonProperty("Token")]
        public String Token { get; set; }
        [JsonProperty("LastName")]
        public String LastName { get; set; }
        [JsonProperty("Nickname")]
        public String NickName { get; set; }
        [JsonProperty("SteamID")]
        public String SteamID { get; set; }
        [JsonProperty("BattleTag")]
        public String BattleTag { get; set; }

        public String ApiPath()
        {
            return UserApiPath.Replace("{/id}", $"/{ID}");
        }

        public string ImageDirectoryPath()
        {
            throw new NotImplementedException();
        }

        public bool IsClassWithImage()
        {
            return false;
        }
    }
}