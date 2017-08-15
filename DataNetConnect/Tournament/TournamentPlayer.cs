using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.IO;

namespace DataNetConnect.ViewModels.Tournament
{
    public class ParticipantViewModel : BaseViewModel
    {
        [JsonProperty("first_name")]
        public String FirstName { get; set; }

        [JsonProperty("last_name")]
        public String LastName { get; set; }

        [JsonProperty("nickname")]
        public String Nickname { get; set; }


        public ParticipantViewModel()
        {

        }
    }
}