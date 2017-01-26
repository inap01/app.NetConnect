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
    public class Tournament : BaseProperties, IApiPath
    {
        private static readonly String TournamentApiPath = @"app/Tournament";
        private static readonly String IconPath = @"Images/Icons/Tournament";
        [ApiPropertyName("")]
        public String Name { get; set; }
        [ApiPropertyName("")]
        public String Icon { get; set; }
        [ApiPropertyName("")]
        public String Rules { get; set; }
        [ApiPropertyName("")]
        public String BattleTag { get; set; }
        [ApiPropertyName("")]
        public String Steam { get; set; }
        [ApiPropertyName("")]
        public Int32 LanID { get; set; }
        [ApiPropertyName("")]
        public Int32 GameID { get; set; }
        [ApiPropertyName("")]
        public Int32 TeamSize { get; set; }
        [ApiPropertyName("")]
        public String Link { get; set; }
        [ApiPropertyName("")]
        public String Mode { get; set; }
        [ApiPropertyName("")]
        public DateTime StartTime { get; set; }
        [ApiPropertyName("")]
        public List<Player> Players { get; set; }
        [ApiPropertyName("")]
        public List<Team> Teams { get; set; }


        public class Player : BaseProperties
        {
            public Int32 UserID { get; set; }
            public Int32 TournamentID { get; set; }
            public Int32 TeamID { get; set; }
            public Boolean Registered { get; set; }
        }
        public class Team : BaseProperties
        {
            public String Name { get; set; }
            public Int32 TournamentID { get; set; }
            public String Password { get; set; }
            public List<Player> Player { get; set; }
        }

        public string ApiPath()
        {
            return TournamentApiPath;
        }
    }
    
}