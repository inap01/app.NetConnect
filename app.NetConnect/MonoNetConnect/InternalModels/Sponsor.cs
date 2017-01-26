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
    public class Sponsor : BaseProperties, IApiPath
    {
        private static String SponsorApiPath = @"app/Partner{/id}";
        private static String SponsorImagePath = @"Images/Sponsor";

        [ApiPropertyName("")]
        public String PartnerTitle { get; set; }
        [ApiPropertyName("")]
        public Boolean ShowPartner { get; set; }
        [ApiPropertyName("")]
        public Boolean ShowFrontsite { get; set; }
        [ApiPropertyName("")]
        public Boolean ShowTrikot { get; set; }
        [ApiPropertyName("")]
        public String Name { get; set; }
        [ApiPropertyName("")]
        public String Content { get; set; }
        [ApiPropertyName("")]
        public String Link { get; set; }
        [ApiPropertyName("")]
        public String Image { get; set; }
        [ApiPropertyName("")]
        public Int32 PartnerPackID { get; set; }
        [ApiPropertyName("")]
        public Boolean Active { get; set; }

        public string ApiPath()
        {
            return SponsorApiPath.Replace("{/id}", $"/{ID}");
        }
    }
}