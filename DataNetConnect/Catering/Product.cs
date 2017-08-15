using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DataNetConnect.ViewModels.Catering
{
    public class ProductViewModel : BaseViewModel
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("image")]
        public String ImageName { get; set; }

        [JsonProperty("price")]
        public Decimal Price { get; set; }

        [JsonProperty("single_choice")]
        public Boolean SingleChoice { get; set; }

        [JsonProperty("attributes")]
        public List<String> Attributes { get; set; }

        public ProductViewModel()
        {

        }
    }
}