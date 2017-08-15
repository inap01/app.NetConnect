using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.IO;

namespace DataNetConnect.ViewModels.Tournament
{
    public class TournamentViewModel : BaseViewModel
    {
        [JsonProperty("lan_id")]
        public Int32 LanID { get; set; }

        [JsonProperty("game_id")]
        public Int32 GameID { get; set; }

        [JsonProperty("team")]
        public Int32 TeamSize { get; set; }

        [JsonProperty("link")]
        public String Link { get; set; }

        [JsonProperty("mode")]
        public String Mode { get; set; }

        [JsonProperty("start")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end")]
        public DateTime EndTime { get; set; }

        [JsonProperty("pause_game")]
        public Boolean IsPauseGame { get; set; }

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

        [JsonProperty("participants_number")]
        public Int32 TeilnehmerAnzahl { get; set; }

        [JsonProperty("player")]
        public List<ParticipantViewModel> Player { get; set; }

        [JsonProperty("teams")]
        public List<TeamViewModel> Teams { get; set; }

        [JsonProperty("powered_by")]
        public String PoweredBy { get; set; }

        [JsonProperty("brand_icon")]
        public String BrandIcon { get; set; }

        [JsonProperty("brand_text")]
        public String BrandText { get; set; }

        public TournamentViewModel()
        {

        }
    }
}