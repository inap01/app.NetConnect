using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataNetConnect.ViewModels.Catering
{
    public class OrderViewModel : BaseViewModel
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("seat")]
        public Int32 SeatID { get; set; }

        [JsonProperty("details")]
        public List<OrderDetailViewModel> Products { get; set; }

        [JsonProperty("price")]
        public Decimal Price { get; set; }

        [JsonProperty("complete_status")]
        public Int32 CompleteStatus { get; set; }

        public OrderViewModel()
        {

        }
    }
}