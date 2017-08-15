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

        public ChangesRequestModel()
        {

        }
        public string ApiPath()
        {
            return ChangesApiPath;
        }

        public DateTime GetLatestChange()
        {
            return DateTime.MinValue;
        }
        public string GetLocalImageName(string FullApiPathImageName)
        {
            return null;
        }
        public string GetLocalImageName()
        {
            return null;
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