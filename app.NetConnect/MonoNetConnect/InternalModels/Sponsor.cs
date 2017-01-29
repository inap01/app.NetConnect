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
        private static String SponsorApiPath = @"app/Partner{/id}";
        private static String SponsorImagePath = @"Images/Sponsor";

        [JsonProperty("")]
        public String PartnerTitle { get; set; }
        [JsonProperty("")]
        public Boolean ShowPartner { get; set; }
        [JsonProperty("")]
        public Boolean ShowFrontsite { get; set; }
        [JsonProperty("")]
        public Boolean ShowTrikot { get; set; }
        [JsonProperty("")]
        public String Name { get; set; }
        [JsonProperty("")]
        public String Content { get; set; }
        [JsonProperty("")]
        public String Link { get; set; }
        [ApiPropertyName("Image")]
        [JsonProperty("")]
        public String Image { get; set; }
        [JsonProperty("")]
        public Int32 PartnerPackID { get; set; }
        [JsonProperty("")]
        public Boolean Active { get; set; }

        public string ApiPath()
        {
            return SponsorApiPath.Replace("{/id}", $"/{ID}");
        }

        public string ImageDirectoryPath()
        {
            return SponsorImagePath;
        }
    }
}