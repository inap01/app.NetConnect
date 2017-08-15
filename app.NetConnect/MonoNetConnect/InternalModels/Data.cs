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

namespace MonoNetConnect.InternalModels
{
    public class Data<T> : List<T>, IApiImageModel
        where T : IApiModels
    {
        public Data()
        {
        }
        public Data(IEnumerable<T> copy)
        {
            this.AddRange(copy);
        }
        public string ApiPath()
        {
            return ((T)Activator.CreateInstance(typeof(T))).ApiPath().Replace("{/id}", "");
        }
        public HashSet<string> GetImages()
        {
            if (typeof(IHasImage).IsAssignableFrom(typeof(T)))
            {
                HashSet<String> images = new HashSet<String>();
                foreach(var x in this)
                {
                    images.Add(((IHasImage)x).GetImage());
                }
                return images;
            }
            return null;
        }
        public DateTime GetLatestChange()
        {
            return this.Count > 0 ? this.Max(x => x.GetLatestChange()) : DateTime.MinValue;
        }
        public string GetImageDirectoryPath()
        {
            return ((T)Activator.CreateInstance(typeof(T))).GetImageDirectoryPath();
        }       
        public bool IsClassWithImage()
        {
            return ((T)Activator.CreateInstance(typeof(T))).IsClassWithImage();
        }
        public string GetLocalImageName(string FullApiPathImageName)
        {
            return ((T)Activator.CreateInstance(typeof(T))).GetLocalImageName(FullApiPathImageName);
        }
        public string GetLocalImageName()
        {
            return ((T)Activator.CreateInstance(typeof(T))).GetLocalImageName();
        }
    }
    
}