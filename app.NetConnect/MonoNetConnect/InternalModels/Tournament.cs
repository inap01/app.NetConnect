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
    public class Tournament : BaseProperties, IApiModels
    {
        private static readonly String TournamentApiPath = @"app/Tournament";
        private static readonly String IconPath = @"Images/Icons/Tournament";
        [JsonProperty("")]
        public String Name { get; set; }
        [JsonProperty("")]
        public String Icon { get; set; }
        [JsonProperty("")]
        public String Rules { get; set; }
        [JsonProperty("")]
        public String BattleTag { get; set; }
        [JsonProperty("")]
        public String Steam { get; set; }
        [JsonProperty("")]
        public Int32 LanID { get; set; }
        [JsonProperty("")]
        public Int32 GameID { get; set; }
        [JsonProperty("")]
        public Int32 TeamSize { get; set; }
        [JsonProperty("")]
        public String Link { get; set; }
        [JsonProperty("")]
        public String Mode { get; set; }
        [JsonProperty("")]
        public DateTime StartTime { get; set; }
        [JsonProperty("")]
        public List<Player> Players { get; set; }
        [JsonProperty("")]
        public List<Team> Teams { get; set; }


        public class Player : BaseProperties, IApiModels
        {
            public Int32 UserID { get; set; }
            public Int32 TournamentID { get; set; }
            public Int32 TeamID { get; set; }
            public Boolean Registered { get; set; }

            public string ApiPath()
            {
                return "";
            }

            public string ImageDirectoryPath()
            {
                throw new NotImplementedException();
            }
        }
        public class Team : BaseProperties, IApiModels
        {
            public String Name { get; set; }
            public Int32 TournamentID { get; set; }
            public String Password { get; set; }
            public List<Player> Player { get; set; }

            public string ApiPath()
            {
                return "";
            }

            public string ImageDirectoryPath()
            {
                throw new NotImplementedException();
            }
        }

        public string ApiPath()
        {
            return TournamentApiPath;
        }

        public string ImageDirectoryPath()
        {
            return IconPath;
        }
    }
    
}