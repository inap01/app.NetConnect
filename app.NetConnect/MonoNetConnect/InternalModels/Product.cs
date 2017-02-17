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
using MonoNetConnect.Cache;
using Newtonsoft.Json;
using MonoNetConnect.Extensions;

namespace MonoNetConnect.InternalModels
{
    
    [Serializable]
    public class Product : BaseProperties, IApiModels, IHasImage, IDeepCloneable<Product>
    {
        private static String ProductImagePath = @"Images/Products";
        private static String ProductApiPath = @"api.php/app/Products{/id}";

        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("description")]
        public String Description { get; set; } = "";
        [JsonProperty("price")]
        public Decimal Price { get; set; }
        [JsonProperty("single_choice")]
        public Boolean SingleChoice { get; set; }
        [ApiPropertyName("Image")]
        [JsonProperty("image")]
        public String ImageName { get; set; }
        [JsonProperty("attributes")]
        public List<String> Attributes { get; set; } = new List<string>();

        public string ApiPath()
        {            
            return ProductApiPath.Replace("{/id}",$"");
        }

        public string GetImageDirectoryPath()
        {
            return ProductImagePath;
        }

        public bool IsClassWithImage()
        {
            return true;
        }      

        public string GetImage()
        {
            return ImageName;
        }
        public Product DeepClone()
        {
            return new Product()
            {
                Attributes = this.Attributes,
                Description = this.Description,
                ID = this.ID,
                ImageName = this.ImageName,
                LatestChange = new DateTime(this.LatestChange.Ticks),
                Name = this.Name,
                Price = this.Price
            };
        }

        object IDeepCloneable.DeepClone()
        {
            return this.DeepClone();
        }
    }
}