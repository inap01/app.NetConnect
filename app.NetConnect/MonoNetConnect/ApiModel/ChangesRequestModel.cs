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
using MonoNetConnect.InternalModels;
using Newtonsoft.Json;

namespace MonoNetConnect.ApiModel
{
    public class ChangesRequestModel : IApiModels
    {
        private String ChangesApiPath = @"api.php/app/Updates";
        [JsonProperty("catering_orders")]
        public DateTime Orders { get; set; }
        [JsonProperty("catering_products")]
        public DateTime Products { get; set; }
        [JsonProperty("feedback")]
        public DateTime Feedback { get; set; }
        [JsonProperty("images")]
        public DateTime Images { get; set; }
        [JsonProperty("logs")]
        public DateTime Logs { get; set; }
        [JsonProperty("news")]
        public DateTime News { get; set; }
        [JsonProperty("news_categories")]
        public DateTime NewsCategories { get; set; }
        [JsonProperty("partner")]
        public DateTime Sponsors { get; set; }
        [JsonProperty("partner_packs")]
        public DateTime SponsorPacks { get; set; }
        [JsonProperty("seating")]
        public DateTime Seating { get; set; }
        [JsonProperty("settings")]
        public DateTime Settings { get; set; }
        [JsonProperty("tournaments")]
        public DateTime Tournaments { get; set; }
        [JsonProperty("tournaments_games")]
        public DateTime TournamentGame { private get; set; }
        [JsonProperty("tournaments_participants")]
        public DateTime TournamentParticipant { private get; set; }
        [JsonProperty("tournaments_teams")]
        public DateTime TournamentTeams { private get; set; }
        [JsonProperty("user")]
        public DateTime user { private get; set; }

        public string ApiPath()
        {
            return ChangesApiPath;
        }

        public DateTime GetLatestChange()
        {
            return DateTime.MinValue;
        }

        public string GetImageDirectoryPath()
        {
            throw new InvalidOperationException();
        }

        public bool IsClassWithImage()
        {
            return false;
        }
    }
}