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
using System.IO;

namespace MonoNetConnect.InternalModels
{
    public class Tournament : BaseProperties, IApiModels
    {
        private static readonly String TournamentApiPath = @"api.php/app/Tournament";
        private static readonly String IconPath = @"Images/Icons/Tournament";
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("icon")]
        public String Icon { get; set; }
        [JsonProperty("rules")]
        public String Rules { get; set; }
        [JsonProperty("battletag")]
        public Boolean BattleTag { get; set; }
        [JsonProperty("steam")]
        public Boolean Steam { get; set; }
        [JsonProperty("lan_id")]
        public Int32 LanID { get; set; }
        [JsonProperty("team")]
        public Int32 TeamSize { get; set; }
        [JsonProperty("link")]
        public String Link { get; set; }
        [JsonProperty("mode")]
        public String Mode { get; set; }
        [JsonProperty("start")]
        public DateTime StartTime { get; set; }
        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }
        [JsonProperty("player")]
        public List<Team> player { get; set; }

        public class Player : BaseProperties, IApiModels
        {
            [JsonProperty("user_id")]
            public Int32 UserID { get; set; }
            [JsonProperty("turnier_id")]
            public Int32 TunierID { get; set; }
            [JsonProperty("tournament_id")]
            public Int32 TournamentID { get; set; }
            [JsonProperty("team_id")]
            public Int32 TeamID { get; set; }
            [JsonProperty("registered")]
            public Boolean Registered { get; set; }

            public string ApiPath()
            {
                return "";
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
        public class Team : BaseProperties, IApiModels
        {
            [JsonProperty("name")]
            public String Name { get; set; }
            [JsonProperty("turnier_id")]
            public Int32 TournamentID { get; set; }
            [JsonProperty("password")]
            public String Password { get; set; }
            [JsonProperty("player")]
            public List<Player> Player { get; set; }

            public string ApiPath()
            {
                return "";
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

        public string ApiPath()
        {
            return Path.Combine(TournamentApiPath, "lan", DataContext.GetDataContext().Settings.Volume);
        }

        public string GetImageDirectoryPath()
        {
            return IconPath;
        }

        public bool IsClassWithImage()
        {
            return new Player().IsClassWithImage() || new Team().IsClassWithImage();
        }
    }
    
}