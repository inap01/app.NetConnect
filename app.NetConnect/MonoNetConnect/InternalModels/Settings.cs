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
    public class Settings : BaseProperties,IApiPath
    {
        private static String SettingsApiPath = @"app/Settings";
        [ApiPropertyName("")]
        public String Volume { get; set; }

        [ApiPropertyName("")]
        public String AdvancePayment { get; set; }

        [ApiPropertyName("")]
        public String BoxOffice { get; set; }

        [ApiPropertyName("")]
        public DateTime Start { get; set; }

        [ApiPropertyName("")]
        public DateTime End { get; set; }

        [ApiPropertyName("")]
        public Boolean ActiveBooking { get; set; }

        [ApiPropertyName("")]
        public Int32 DaysBooked { get; set; }

        [ApiPropertyName("")]
        public String IBan { get; set; }

        [ApiPropertyName("")]
        public String BIC { get; set; }

        public string ApiPath()
        {
            return SettingsApiPath;
        }
    }
}