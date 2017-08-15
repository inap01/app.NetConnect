using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.IO;

namespace DataNetConnect.ViewModels.Tournament
{
    public class TeamViewModel : BaseViewModel
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("password")]
        public String Password { get; set; }

        [JsonProperty("player")]
        public List<ParticipantViewModel> Player { get; set; }

        public TeamViewModel()
        {

        }
    }
}