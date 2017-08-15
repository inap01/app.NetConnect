using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DataNetConnect.ViewModels.Partner
{
    public class PartnerViewModel : BaseViewModel
    {
        #region Properties
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("link")]
        public String Link { get; set; }

        [JsonProperty("content")]
        public String Content { get; set; }

        [JsonProperty("image_path")]
        public String ImagePath { get; set; }

        [JsonProperty("icon")]
        public String Icon { get; set; }

        [JsonProperty("image")]
        public String Image { get; set; }

        [JsonProperty("image_alt")]
        public String ImageAlt { get; set; }

        [JsonProperty("status")]
        public Int32? Status { get; set; }

        [JsonProperty("active")]
        public Boolean? Active { get; set; }

        [JsonProperty("partner_title")]
        public String PartnerTitle { get; set; }

        [JsonProperty("show_partner")]
        public Boolean? ShowPartner { get; set; }

        [JsonProperty("show_frontsite")]
        public Boolean? ShowFrontsite { get; set; }

        [JsonProperty("show_trikot")]
        public Boolean? ShowTrikot { get; set; }
#endregion
        public PartnerViewModel()
        {

        }
    }
}