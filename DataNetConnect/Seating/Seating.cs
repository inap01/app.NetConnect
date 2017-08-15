using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DataNetConnect.ViewModels.Seating
{
    public class SeatingViewModel : BaseViewModel
    {
        [JsonProperty("user_id")]
        public Int32 UserID { get; set; }

        [JsonProperty("status")]
        public Int32 Status { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("date")]
        public DateTime ReservationDate { get; set; }

        [JsonProperty("payed")]
        public String Payed { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("first_name")]
        public String FirstName { get; set; }

        [JsonProperty("last_name")]
        public String LastName { get; set; }

        [JsonProperty("nickname")]
        public String Nickname { get; set; }

        public SeatingViewModel()
        {

        }
    }
}