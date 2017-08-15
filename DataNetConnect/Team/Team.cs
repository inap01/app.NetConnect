using DataNetConnect.ViewModels.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNetConnect.Team
{
    class TeamViewModel : BaseViewModel
    {
        [JsonProperty("team_count")]
        public Int32 TeamCount { get; set; }

        [JsonProperty("vorstand")]
        public List<UserViewModel_Frontend> Vorstand { get; set; }

        [JsonProperty("mitglieder")]
        public List<UserViewModel_Frontend> Mitglieder { get; set; }

        public TeamViewModel()
        {

        }
    }
}
