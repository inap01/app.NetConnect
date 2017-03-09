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
using Newtonsoft.Json;

namespace MonoNetConnect.InternalModels
{
        public class Seat : BaseProperties, IApiModels
    {
        private static readonly String SeatingApiPath = @"api.php/app/Seating";

        [JsonProperty("user_id")]
        public int UserID { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("description")]
        public String Description { get; set; }
        [JsonProperty("date")]
        public DateTime ReservationDate { get; set; }
        [JsonProperty("payed")]
        public int Payed { get; set; }
        [JsonProperty("email")]
        public String EMail { get; set; }
        [JsonProperty("first_name")]
        public String FirstName { get; set; }
        [JsonProperty("last_name")]
        public String LastName { get; set; }
        [JsonProperty("nickname")]
        public String Nickname { get; set; }
        
        public string ApiPath()
        {
            return SeatingApiPath;
        }

        public string GetImageDirectoryPath()
        {
            return "";
        }

        public DateTime GetLatestChange()
        {
            return LatestChange;
        }

        public bool IsClassWithImage()
        {
            return false;
        }
    }
}