using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataNetConnect.ViewModels.Catering
{
    public class OrderDetailViewModel : BaseViewModel
    {
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("count")]
        public Int32 Count { get; set; }
        [JsonProperty("attributes")]
        public List<String> Attributes { get; set; }

        public OrderDetailViewModel()
        {

        }
    }
}