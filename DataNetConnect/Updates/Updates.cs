using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataNetConnect.ViewModels.Updates
{
    class UpdatesViewModel
    {
        [JsonProperty("catering_orders")]
        public DateTime Orders { get; set; }

        [JsonProperty("catering_products")]
        public DateTime Products { get; set; }

        [JsonProperty("chat")]
        public DateTime Chat { get; set; }

        [JsonProperty("logs")]
        public DateTime Logs { get; set; }

        [JsonProperty("news")]
        public DateTime News { get; set; }

        [JsonProperty("news_categories")]
        public DateTime NewsCategories { get; set; }

        [JsonProperty("partner")]
        public DateTime Sponsor { get; set; }

        [JsonProperty("partner_packs")]
        public DateTime SponsorPack { get; set; }

        [JsonProperty("seating")]
        public DateTime Seating { get; set; }

        [JsonProperty("settings")]
        public DateTime Settings { get; set; }

        [JsonProperty("tournaments")]
        public DateTime Tournaments { get; set; }

        [JsonProperty("tournaments_games")]
        public DateTime TournamentGames { get; set; }

        [JsonProperty("tournaments_participants")]
        public DateTime TournamentParticipants { get; set; }

        [JsonProperty("tournaments_teams")]
        public DateTime TournamentTeams { get; set; }

        [JsonProperty("user")]
        public DateTime User { get; set; }
        public UpdatesViewModel()
        {

        }
    }
}
