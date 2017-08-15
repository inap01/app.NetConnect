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
using DataNetConnect.ViewModels.Catering;

namespace MonoNetConnect.InternalModels
{
    public class OrderProduct : OrderDetailViewModel
    {
        [JsonIgnore]
        public decimal Price { get; set; }
        [JsonIgnore]
        protected override DateTime LatestChange { get; set; }

        public OrderProduct()
        {

        }
        public OrderProduct(Product p)
        {
            Attributes = p.Attributes;
            Price = p.Price;
            Count = 1;
            Name = p.Name;

        }
        public static implicit operator OrderProduct(Product p)
        {
            return new OrderProduct
            {
                
                Attributes = p.Attributes,
                Price = p.Price,
                Count = 1,
                Name = p.Name
            };
        }
        public bool Equals(Product obj)
        {
            bool attEqual = true;
            if (obj.Attributes.Count == this.Attributes.Count)
                foreach (var att in obj.Attributes)
                    if (!this.Attributes.Contains(att))
                        attEqual = false;
            return (obj.Name == this.Name && attEqual);
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
    class Order
    {
        private static String OrderApiPath = @"api.php/app/Products{/id}";
        
        [JsonProperty("user_id")]
        public int UserID { get; set; }
        [JsonProperty("seat_id")]
        public int SeatID { get; set; }
        [JsonProperty("order")]
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