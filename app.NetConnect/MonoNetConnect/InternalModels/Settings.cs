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
    public class Settings : BaseProperties,IApiModels
    {
        private static String SettingsApiPath = @"app/Settings";
        [JsonProperty("")]
        public String Volume { get; set; }

        [JsonProperty("")]
        public String AdvancePayment { get; set; }

        [JsonProperty("")]
        public String BoxOffice { get; set; }

        [JsonProperty("")]
        public DateTime Start { get; set; }

        [JsonProperty("")]
        public DateTime End { get; set; }

        [JsonProperty("")]
        public Boolean ActiveBooking { get; set; }

        [JsonProperty("")]
        public Int32 DaysBooked { get; set; }

        [JsonProperty("")]
        public String IBan { get; set; }

        [JsonProperty("")]
        public String BIC { get; set; }

        public string ApiPath()
        {
            return SettingsApiPath;
        }

        public string ImageDirectoryPath()
        {
            throw new NotImplementedException();
        }
    }
}