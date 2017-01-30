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
    public class Sponsor : BaseProperties, IApiModels
    {
        private static String SponsorApiPath = @"api.php/app/Partner";
        private static String SponsorImagePath = @"Images/Sponsor";

        [JsonProperty("partner_title")]
        public String PartnerTitle { get; set; }
        [JsonProperty("show_partner")]
        public Boolean ShowPartner { get; set; }
        [JsonProperty("show_frontsite")]
        public Boolean ShowFrontsite { get; set; }
        [JsonProperty("show_trikot")]
        public Boolean ShowTrikot { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("content")]
        public String Content { get; set; }
        [JsonProperty("link")]
        public String Link { get; set; }
        [ApiPropertyName("Image")]
        [JsonProperty("image")]
        public String Image { get; set; }
        [JsonProperty("status")]
        public Int32 PartnerPackID { get; set; }
        [JsonProperty("active")]
        public Boolean Active { get; set; }

        public string ApiPath()
        {
            return SponsorApiPath;
        }

        public string ImageDirectoryPath()
        {
            return SponsorImagePath;
        }

        public bool IsClassWithImage()
        {
            return true;
        }
    }
}