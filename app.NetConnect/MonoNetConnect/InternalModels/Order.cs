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
    public class OrderProduct : BaseProperties, IApiModels
    {
        [JsonProperty("attributes")]
        public List<String> Attributes { get; set; }
        [JsonIgnore]
        public decimal Price { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }

        public static implicit operator OrderProduct(Product p)
        {
            return new OrderProduct
            {
                Attributes = p.Attributes,
                ID = p.ID,
                Price = p.Price,
                Count = 1
            };
        }

        public string ApiPath()
        {
            return null;
        }

        public string GetImageDirectoryPath()
        {
            return null;
        }

        public bool IsClassWithImage()
        {
            return false;
        }
    }
    class Order : BaseProperties, IApiModels
    {
        private static String OrderApiPath = @"api.php/app/Products{/id}";
        
        [JsonProperty("user_id")]
        public int UserID { get; set; }
        [JsonProperty("seat_id")]
        public int SeatID { get; set; }
        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>();

        public string ApiPath()
        {
            return OrderApiPath.Replace("{/id}", $"");
        }

        public string GetImageDirectoryPath()
        {
            return "";
        }

        public bool IsClassWithImage()
        {
            return false;
        }
    }
}