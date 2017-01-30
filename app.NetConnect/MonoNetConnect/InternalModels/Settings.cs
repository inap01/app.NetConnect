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
        private static String SettingsApiPath = @"api.php";
        [JsonProperty("colume")]
        public String Volume { get; set; }

        [JsonProperty("vorkasse")]
        public String AdvancePayment { get; set; }

        [JsonProperty("abendkasse")]
        public String BoxOffice { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("ende")]
        public DateTime End { get; set; }

        [JsonProperty("active_reservierung")]
        public Boolean ActiveBooking { get; set; }

        [JsonProperty("kontocheck")]
        public Int32 DaysBooked { get; set; }

        [JsonProperty("iban")]
        public String IBan { get; set; }

        [JsonProperty("bic")]
        public String BIC { get; set; }
        public String BLZ { private get; set; }
        public String Kontonummer { private get; set; }

        public string ApiPath()
        {
            return SettingsApiPath;
        }

        public string ImageDirectoryPath()
        {
            throw new NotImplementedException();
        }

        public bool IsClassWithImage()
        {
            return false;
        }
    }
}