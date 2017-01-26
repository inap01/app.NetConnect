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

namespace MonoNetConnect.InternalModels
{
    public class User: BaseProperties, IApiPath
    {        
        public static String UserApiPath = @"app/User{/id}";

        [ApiPropertyName("")]
        public String FirstName { get; set; }
        [ApiPropertyName("")]
        public String Token { get; set; }
        [ApiPropertyName("")]
        public String LastName { get; set; }
        [ApiPropertyName("")]
        public String NickName { get; set; }
        [ApiPropertyName("")]
        public String SteamID { get; set; }
        [ApiPropertyName("")]
        public String BattleTag { get; set; }

        public String ApiPath()
        {
            return UserApiPath.Replace("{/id}", $"/{ID}");
        }
    }
}