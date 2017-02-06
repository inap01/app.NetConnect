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
        private static String ProductImagePath = @"Images/Product";
        private static String ProductApiPath = @"api.php/app/Product{/id}";

        [JsonProperty("")]
        public String Name { get; set; }
        [JsonProperty("")]
        public String Description { get; set; } = "";
        [JsonProperty("")]
        public Decimal Price { get; set; }
        [ApiPropertyName("Image")]
        [JsonProperty("")]
        public String ImageName { get; set; }
        [JsonProperty("")]
        public List<String> Attributes { get; set; } = new List<string>();

        public string ApiPath()
        {            
            return ProductApiPath.Replace("{/id}",$"/{ID}");
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