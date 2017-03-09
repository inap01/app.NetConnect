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
using MonoNetConnect.Extensions;

namespace MonoNetConnect.InternalModels
{
    public class Settings : BaseProperties,IApiModels, IDeepCloneable<Settings>
    {
        private static String SettingsApiPath = @"api.php";
        [JsonProperty("volume")]
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
        public DateTime KontoChecked { get; set; }

        [JsonProperty("tage_reservierung")]
        public Int32 DaysReserved { get; set; }

        [JsonProperty("iban")]
        public String IBan { get; set; }

        [JsonProperty("bic")]
        public String BIC { get; set; }
        [JsonProperty("blz")]
        public String BLZ { private get; set; }
        [JsonProperty("kontonummer")]
        public String Kontonummer { private get; set; }

        public string ApiPath()
        {
            return SettingsApiPath;
        }

        public string GetImageDirectoryPath()
        {
            throw new NotImplementedException();
        }

        public bool IsClassWithImage()
        {
            return false;
        }

        public Settings DeepClone()
        {
            return new Settings()
            {
                ActiveBooking = this.ActiveBooking,
                BIC = this.BIC,
                BLZ = this.BLZ,
                AdvancePayment = this.AdvancePayment,
                BoxOffice = this.BoxOffice,
                DaysReserved = this.DaysReserved,
                End = new DateTime(this.End.Ticks),
                Start = new DateTime(this.Start.Ticks),
                ID = this.ID,
                IBan = this.IBan,
                KontoChecked = new DateTime(this.KontoChecked.Ticks),
                Kontonummer = this.Kontonummer,
                LatestChange = this.LatestChange,
                Volume = this.Volume,
            };
        }

        object IDeepCloneable.DeepClone()
        {
            return this.DeepClone();
        }
    }
}