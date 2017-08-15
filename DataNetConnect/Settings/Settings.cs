using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DataNetConnect.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        [JsonProperty("volume")]
        public Int32 Volume { get; set; }

        [JsonProperty("vorkasse")]
        public Int32 AdvancePayment { get; set; }

        [JsonProperty("abendkasse")]
        public Int32 BoxOffice { get; set; }

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

        [JsonProperty("catering")]
        public Boolean Catering { get; set; }

        [JsonProperty("feedback")]
        public Boolean Feedback { get; set; }

        [JsonProperty("feedback_link")]
        public String FeedbackLink { get; set; }

        [JsonProperty("chat")]
        public Boolean Chat { get; set; }

        [JsonProperty("kontoinhaber")]
        public String Kontoinhaber { get; set; }

        [JsonProperty("iban")]
        public String IBan { get; set; }

        [JsonProperty("blz")]
        public String BLZ { get; set; }

        [JsonProperty("kontonummer")]
        public String Kontonummer { get; set; }

        [JsonProperty("bic")]
        public String BIC { get; set; }

        public SettingsViewModel()
        {

        }
    }
}